//-----------------------------------------------------------------------------
// VideoPlayer.cs
// Programiranje: Ivan Šoštarko (ivan.sostarko@hotmail.com)
// Dizajn Ruben: Petran (rubenpetran@gmail.com)
// Copyright (C)219 GAME STUDIO. All rights reserved.
//-----------------------------------------------------------------------------
using System;
using System.Threading;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using DirectShowLib;

namespace SeeSharp.Xna.Video
{

public enum VideoState
{
    Playing,
    Paused,
    Stopped
}


public class VideoPlayer : ISampleGrabberCB, IDisposable
{
    #region Media Type GUIDs
    private Guid MEDIATYPE_Video = new Guid(0x73646976, 0x0000, 0x0010, 0x80, 0x00, 0x00, 0xaa, 0x00, 0x38, 0x9b, 0x71);
    private Guid MEDIASUBTYPE_RGB24 = new Guid(0xe436eb7d, 0x524f, 0x11ce, 0x9f, 0x53, 0x00, 0x20, 0xaf, 0x0b, 0xa7, 0x70);
    private Guid FORMAT_VideoInfo = new Guid(0x05589f80, 0xc356, 0x11ce, 0xbf, 0x01, 0x00, 0xaa, 0x00, 0x55, 0x59, 0x5a);
    #endregion

    #region Private Fields

    private FilterGraph fg = null;


    private IGraphBuilder gb = null;

    private IMediaControl mc = null;


    private IMediaEventEx me = null;


    private IMediaPosition mp = null;


    private IMediaSeeking ms = null;


    private Thread updateThread;


    private Thread waitThread;

    private string filename;


    private bool frameAvailable = false;


    private byte[] bgrData;


    private byte[] videoFrameBytes;


    private int videoWidth = 0;


    private int videoHeight = 0;


    private Texture2D outputFrame;

    private long avgTimePerFrame;


    private int bitRate;


    private VideoState currentState;


    private bool isDisposed = false;


    private long currentPosition;

    private long videoDuration;


    private byte alphaTransparency = 255;
    #endregion

    #region Public Properties

    public Texture2D OutputFrame
    {
        get
        {
            return outputFrame;
        }
    }


    public int VideoWidth
    {
        get
        {
            return videoWidth;
        }
    }


    public int VideoHeight
    {
        get
        {
            return videoHeight;
        }
    }


    public double CurrentPosition
    {
        get
        {
            return (double)currentPosition / 10000000;
        }
        set
        {
            if (value < 0)
                value = 0;

            if (value > Duration)
                value = Duration;

            DsError.ThrowExceptionForHR(mp.put_CurrentPosition(value));
            currentPosition = (long)value * 10000000;
        }
    }


    public string CurrentPositionAsTimeString
    {
        get
        {
            double seconds = (double)currentPosition / 10000000;

            double minutes = seconds / 60;
            double hours = minutes / 60;

            int realHours = (int)Math.Floor(hours);
            minutes -= realHours * 60;

            int realMinutes = (int)Math.Floor(minutes);
            seconds -= realMinutes * 60;

            int realSeconds = (int)Math.Floor(seconds);

            return (realHours < 10 ? "0" + realHours.ToString() : realHours.ToString()) + ":" + (realMinutes < 10 ? "0" + realMinutes.ToString() : realMinutes.ToString()) + ":" + (realSeconds < 10 ? "0" + realSeconds.ToString() : realSeconds.ToString());
        }
    }

    public double Duration
    {
        get
        {
            return (double)videoDuration / 10000000;
        }
    }


    public string DurationAsTimeString
    {
        get
        {
            double seconds = (double)videoDuration / 10000000;

            double minutes = seconds / 60;
            double hours = minutes / 60;

            int realHours = (int)Math.Floor(hours);
            minutes -= realHours * 60;

            int realMinutes = (int)Math.Floor(minutes);
            seconds -= realMinutes * 60;

            int realSeconds = (int)Math.Floor(seconds);

            return (realHours < 10 ? "0" + realHours.ToString() : realHours.ToString()) + ":" + (realMinutes < 10 ? "0" + realMinutes.ToString() : realMinutes.ToString()) + ":" + (realSeconds < 10 ? "0" + realSeconds.ToString() : realSeconds.ToString());
        }
    }


    public string FileName
    {
        get
        {
            return filename;
        }
    }

    public VideoState CurrentState
    {
        get
        {
            return currentState;
        }
        set
        {
            switch (value)
            {
            case VideoState.Playing:
                Play();
                break;
            case VideoState.Paused:
                Pause();
                break;
            case VideoState.Stopped:
                Stop();
                break;
            }
        }
    }


    public event EventHandler OnVideoComplete;


    public bool IsDisposed
    {
        get
        {
            return isDisposed;
        }
    }


    public int FramesPerSecond
    {
        get
        {
            if (avgTimePerFrame == 0)
                return -1;

            float frameTime = (float)avgTimePerFrame / 10000000.0f;
            float framesPS = 1.0f / frameTime;
            return (int)Math.Round(framesPS, 0, MidpointRounding.ToEven);
        }
    }


    public float MillisecondsPerFrame
    {
        get
        {
            if (avgTimePerFrame == 0)
                return -1;

            return (float)avgTimePerFrame / 10000.0f;
        }
    }


    public byte AlphaTransparency
    {
        get
        {
            return alphaTransparency;
        }
        set
        {
            alphaTransparency = value;
        }
    }
    #endregion

    #region Constructor
    /// <summary>
    /// Creates a new Video Player. Automatically creates the required Texture2D on the specificied GraphicsDevice.
    /// </summary>
    /// <param name="FileName">The video file to open</param>
    /// <param name="graphicsDevice">XNA Graphics Device</param>
    public VideoPlayer(string FileName, GraphicsDevice graphicsDevice)
    {
        try
        {

            currentState = VideoState.Stopped;

            filename = FileName;


            InitInterfaces();


            SampleGrabber sg = new SampleGrabber();
            ISampleGrabber sampleGrabber = (ISampleGrabber)sg;
            DsError.ThrowExceptionForHR(gb.AddFilter((IBaseFilter)sg, "Grabber"));


            AMMediaType mt = new AMMediaType();
            mt.majorType = MEDIATYPE_Video;     // Video
            mt.subType = MEDIASUBTYPE_RGB24;    // RGB24
            mt.formatType = FORMAT_VideoInfo;   // VideoInfo
            DsError.ThrowExceptionForHR(sampleGrabber.SetMediaType(mt));


            DsError.ThrowExceptionForHR(gb.RenderFile(filename, null));


            DsError.ThrowExceptionForHR(sampleGrabber.SetBufferSamples(true));
            DsError.ThrowExceptionForHR(sampleGrabber.SetOneShot(false));
            DsError.ThrowExceptionForHR(sampleGrabber.SetCallback((ISampleGrabberCB)this, 1));


            IVideoWindow pVideoWindow = (IVideoWindow)gb;
            DsError.ThrowExceptionForHR(pVideoWindow.put_AutoShow(OABool.False));


            AMMediaType MediaType = new AMMediaType();
            DsError.ThrowExceptionForHR(sampleGrabber.GetConnectedMediaType(MediaType));
            VideoInfoHeader pVideoHeader = new VideoInfoHeader();
            Marshal.PtrToStructure(MediaType.formatPtr, pVideoHeader);

            videoHeight = pVideoHeader.BmiHeader.Height;
            videoWidth = pVideoHeader.BmiHeader.Width;
            avgTimePerFrame = pVideoHeader.AvgTimePerFrame;
            bitRate = pVideoHeader.BitRate;
            DsError.ThrowExceptionForHR(ms.GetDuration(out videoDuration));


            videoFrameBytes = new byte[(videoHeight * videoWidth) * 4]; // RGBA format (4 bytes per pixel)
            bgrData = new byte[(videoHeight * videoWidth) * 3];         // BGR24 format (3 bytes per pixel)


            outputFrame = new Texture2D(graphicsDevice, videoWidth, videoHeight, 1, TextureUsage.None, SurfaceFormat.Color);
        }
        catch
        {
            throw new Exception("Unable to Load or Play the video file");
        }
    }
    #endregion

    #region DirectShow Interface Management
    /// <summary>
    /// Initialises DirectShow interfaces
    /// </summary>
    private void InitInterfaces()
    {
        fg = new FilterGraph();
        gb = (IGraphBuilder)fg;
        mc = (IMediaControl)fg;
        me = (IMediaEventEx)fg;
        ms = (IMediaSeeking)fg;
        mp = (IMediaPosition)fg;
    }


    private void CloseInterfaces()
    {
        if (me != null)
        {
            DsError.ThrowExceptionForHR(mc.Stop());
            //0x00008001 = WM_GRAPHNOTIFY
            DsError.ThrowExceptionForHR(me.SetNotifyWindow(IntPtr.Zero, 0x00008001, IntPtr.Zero));
        }
        mc = null;
        me = null;
        gb = null;
        ms = null;
        mp = null;
        if (fg != null)
            Marshal.ReleaseComObject(fg);
        fg = null;
    }
    #endregion

    #region Update and Media Control
    /// <summary>
    /// Updates the Output Frame data using data from the video stream. Call this in Game.Update().
    /// </summary>
    public void Update()
    {
        // Remove the OutputFrame from the GraphicsDevice to prevent an InvalidOperationException on the SetData line.
        if (outputFrame.GraphicsDevice.Textures[0] == outputFrame)
        {
            outputFrame.GraphicsDevice.Textures[0] = null;
        }

        // Set video data into the Output Frame
        outputFrame.SetData<byte>(videoFrameBytes);

        // Update current position read-out
        DsError.ThrowExceptionForHR(ms.GetCurrentPosition(out currentPosition));
    }

    /// <summary>
    /// Starts playing the video
    /// </summary>
    public void Play()
    {
        if (currentState != VideoState.Playing)
        {
            // Create video threads
            updateThread = new Thread(new ThreadStart(UpdateBuffer));
            waitThread = new Thread(new ThreadStart(WaitForCompletion));

            // Start the FilterGraph
            DsError.ThrowExceptionForHR(mc.Run());

            // Start Threads
            updateThread.Start();
            waitThread.Start();

            // Update VideoState
            currentState = VideoState.Playing;
        }
    }

    /// <summary>
    /// Pauses the video
    /// </summary>
    public void Pause()
    {
        // End threads
        if (updateThread != null)
            updateThread.Abort();
        updateThread = null;

        if (waitThread != null)
            waitThread.Abort();
        waitThread = null;

        // Stop the FilterGraph (but remembers the current position)
        DsError.ThrowExceptionForHR(mc.Stop());

        // Update VideoState
        currentState = VideoState.Paused;
    }

    /// <summary>
    /// Stops playing the video
    /// </summary>
    public void Stop()
    {
        // End Threads
        if (updateThread != null)
            updateThread.Abort();
        updateThread = null;

        if (waitThread != null)
            waitThread.Abort();
        waitThread = null;

        // Stop the FilterGraph
        DsError.ThrowExceptionForHR(mc.Stop());

        // Reset the current position
        DsError.ThrowExceptionForHR(ms.SetPositions(0, AMSeekingSeekingFlags.AbsolutePositioning, 0, AMSeekingSeekingFlags.NoPositioning));

        // Update VideoState
        currentState = VideoState.Stopped;
    }

    /// <summary>
    /// Rewinds the video to the start and plays it again
    /// </summary>
    public void Rewind()
    {
        Stop();
        Play();
    }
    #endregion

    #region ISampleGrabberCB Members and Helpers
    /// <summary>
    /// Required public callback from DirectShow SampleGrabber. Do not call this method.
    /// </summary>
    public int BufferCB(double SampleTime, IntPtr pBuffer, int BufferLen)
    {
        // Copy raw data into bgrData byte array
        Marshal.Copy(pBuffer, bgrData, 0, BufferLen);

        // Flag the new frame as available
        frameAvailable = true;

        // Return S_OK
        return 0;
    }

    /// <summary>
    /// Required public callback from DirectShow SampleGrabber. Do not call this method.
    /// </summary>
    public int SampleCB(double SampleTime, IMediaSample pSample)
    {
        // Return S_OK
        return 0;
    }

    /// <summary>
    /// Worker to copy the BGR data from the video stream into the RGBA byte array for the Output Frame.
    /// </summary>
    private void UpdateBuffer()
    {
        int waitTime = avgTimePerFrame != 0 ? (int)((float)avgTimePerFrame / 10000) : 20;

        int samplePosRGBA = 0;
        int samplePosRGB24 = 0;

        while (true)
        {
            for (int y = 0, y2 = videoHeight - 1; y < videoHeight; y++, y2--)
            {
                for (int x = 0; x < videoWidth; x++)
                {
                    samplePosRGBA = (((y2 * videoWidth) + x) * 4);
                    samplePosRGB24 = ((y * videoWidth) + x) * 3;

                    videoFrameBytes[samplePosRGBA + 0] = bgrData[samplePosRGB24 + 0];
                    videoFrameBytes[samplePosRGBA + 1] = bgrData[samplePosRGB24 + 1];
                    videoFrameBytes[samplePosRGBA + 2] = bgrData[samplePosRGB24 + 2];
                    videoFrameBytes[samplePosRGBA + 3] = alphaTransparency;
                }
            }

            frameAvailable = false;
            while (!frameAvailable)
            { Thread.Sleep(waitTime); }
        }
    }

    /// <summary>
    /// Waits for the video to finish, then calls the OnVideoComplete event
    /// </summary>
    private void WaitForCompletion()
    {
        int waitTime = avgTimePerFrame != 0 ? (int)((float)avgTimePerFrame / 10000) : 20;

        try
        {
            while (videoDuration > currentPosition)
            {
                Thread.Sleep(waitTime);
            }

            if (OnVideoComplete != null)
                OnVideoComplete.Invoke(this, EventArgs.Empty);
        }
        catch { }
    }
    #endregion

    #region IDisposable Members
    /// <summary>
    /// Cleans up the Video Player. Must be called when finished with the player.
    /// </summary>
    public void Dispose()
    {
        isDisposed = true;

        Stop();
        CloseInterfaces();

        outputFrame.Dispose();
        outputFrame = null;

    }
    #endregion
}
}