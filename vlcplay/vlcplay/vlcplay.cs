using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Runtime.ExceptionServices;
using System.Security;
using System.IO;
using System.Threading;
using System.Windows.Forms;

/// <summary>
/// 视频播放类
/// </summary>
namespace vlcplay
{
    using libvlc_media_t = System.IntPtr;
    using libvlc_media_player_t = System.IntPtr;
    using libvlc_instance_t = System.IntPtr;
    public partial class VLCPlayer
    {
        
        #region 全局变量
        //数组转换为指针  
        internal struct PointerToArrayOfPointerHelper
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 11)]
            public IntPtr[] pointers;
        }

        //vlc库启动参数配置  
        private static string pluginPath = System.Environment.CurrentDirectory + "\\plugins\\";
        // private static string pluginPath = "D:\\VLCTools\\plugins\\";
        private static string plugin_arg = "--plugin-path=" + pluginPath;
        //用于播放节目时，转录节目  
        //private static string program_arg = "--sout=#duplicate{dst=std{access=file,mux=ts,dst=d:/test.ts}}";  
        private static string[] arguments = { "-I", "dummy", "--ignore-config", "--video-title", plugin_arg };//, program_arg };  
        //private static string[] arguments = { "--verbose=2", "--network-caching=300", "--no-snapshot-preview" ,plugin_arg};

        #region 结构体
        public struct libvlc_media_stats_t
        {
            /* Input */
            public int i_read_bytes;
            public float f_input_bitrate;

            /* Demux */
            public int i_demux_read_bytes;
            public float f_demux_bitrate;
            public int i_demux_corrupted;
            public int i_demux_discontinuity;

            /* Decoders */
            public int i_decoded_video;
            public int i_decoded_audio;

            /* Video Output */
            public int i_displayed_pictures;
            public int i_lost_pictures;

            /* Audio output */
            public int i_played_abuffers;
            public int i_lost_abuffers;

            /* Stream output */
            public int i_sent_packets;
            public int i_sent_bytes;
            public float f_send_bitrate;
        }
        #endregion

        #endregion
        #region 私有变量
        private libvlc_instance_t lit;
        private libvlc_media_player_t lmpt;
        #endregion
        #region 公有函数
        /// <summary>
        /// 播放网络视频流
        /// </summary>
        /// <param name="url">url地址</param>
        /// <param name="handle">显示控件句柄</param>
        /// <returns>true：播放成功；false：播放失败</returns>
        public bool playUrl(string url, IntPtr handle)
        {
            lit = Create_Media_Instance();
            lmpt = Create_MediaPlayer(lit, handle);
            //播放网络视频
            return NetWork_Media_Play(lit, lmpt, url);
            //播放本地视频
            // return Local_Media_Play(lit, lmpt, url);
        }
        /// <summary>
        /// 播放本地视频
        /// </summary>
        /// <param name="path">视频路径</param>
        /// <param name="handle">显示控件句柄</param>
        /// <returns>true：播放成功；false：播放失败</returns>
        public bool playLocalVideo(string path, IntPtr handle)
        {
            lit = Create_Media_Instance();
            lmpt = Create_MediaPlayer(lit, handle);
            return Local_Media_Play(lit, lmpt, path);
        }
        /// <summary>
        /// 释放VLC资源
        /// </summary>
        /// <returns>true：释放；false：失败</returns>
        public bool release()
        {
            try
            {
                MediaPlayer_Stop(lmpt);
                // Release_Media_Instance(lit);
                Release_MediaPlayer(lmpt);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("关闭vlc失败：" + ex.ToString());
                return false;
            }
        }
        /// <summary>
        /// 是否正在播放
        /// </summary>
        /// <returns></returns>
        public bool IsPlaying()
        {
            return MediaPlayer_IsPlaying(lmpt);
        }
        /// <summary>
        /// 停止播放
        /// </summary>
        public void Stop()
        {
            MediaPlayer_Stop(lmpt);
        }
        /// <summary>
        /// 获得视频时长
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public double Durations(string url)
        {
            return Duration(lit, url);
        }

        /// <summary>
        /// 暂停播放
        /// </summary>
        public void Pause()
        {
            MediaPlayer_Pause(lmpt);
        }
        /// <summary>
        /// 播放
        /// </summary>
        public void PlayU()
        {
            Play(lmpt);
        }
        /// <summary>
        /// 设置播放时间
        /// </summary>
        /// <param name="seekTime"></param>
        public void SetTime(double seekTime)
        {
            if (lmpt != IntPtr.Zero)
            {
                SafeNativeMethods.libvlc_media_player_set_time(lmpt, (Int64)seekTime * 1000);
            }
        }
        public void Aspect(string aspects)
        {
            if (lmpt != IntPtr.Zero)
            {
                SafeNativeMethods.libvlc_video_set_aspect_ratio(lmpt, aspects.ToCharArray());
            }
        }
        /// <summary>
        /// 获得播放时间
        /// </summary>
        /// <returns></returns>
        public double GetTime()
        {
            double seekTime = 0;
            if (lmpt != IntPtr.Zero)
            {
                seekTime = SafeNativeMethods.libvlc_media_player_get_time(lmpt);
            }
            return seekTime;
        }
        /// <summary>
        ///获取屏幕参数
        /// </summary>
        /// <returns></returns>
        public int GetFullscreen()
        {
            return SafeNativeMethods.libvlc_get_fullscreen(lmpt);
        }
        public bool SetFullscreen(int screen)
        {
            return SetFullScreen(lmpt, screen);
        }
        /// <summary>
        /// 录制快照
        /// </summary>
        /// <param name="path"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool TakeSnapshots(string path, string name)
        {
            return TakeSnapShot(lmpt, path, name);
        }
        #endregion
        #region 私有函数
        /// <summary>  
        /// 创建VLC播放资源索引  
        /// </summary>  
        /// <param name="arguments"></param>  
        /// <returns></returns>  
        private libvlc_instance_t Create_Media_Instance()
        {
            libvlc_instance_t libvlc_instance = IntPtr.Zero;
            IntPtr argvPtr = IntPtr.Zero;

            try
            {
                if (arguments.Length == 0 ||
                    arguments == null)
                {
                    return IntPtr.Zero;
                }

                //将string数组转换为指针  
                argvPtr = StrToIntPtr(arguments);
                if (argvPtr == null || argvPtr == IntPtr.Zero)
                {
                    return IntPtr.Zero;
                }

                //设置启动参数  
                libvlc_instance = SafeNativeMethods.libvlc_new(arguments.Length, argvPtr);
                if (libvlc_instance == null || libvlc_instance == IntPtr.Zero)
                {
                    return IntPtr.Zero;
                }

                return libvlc_instance;
            }
            catch
            {
                return IntPtr.Zero;
            }
        }

        /// <summary>  
        /// 释放VLC播放资源索引  
        /// </summary>  
        /// <param name="libvlc_instance">VLC 全局变量</param>  
        private void Release_Media_Instance(libvlc_instance_t libvlc_instance)
        {
            try
            {
                if (libvlc_instance != IntPtr.Zero ||
                    libvlc_instance != null)
                {
                    SafeNativeMethods.libvlc_release(libvlc_instance);
                }

                libvlc_instance = IntPtr.Zero;
            }
            catch (Exception)
            {
                libvlc_instance = IntPtr.Zero;
            }
        }

        /// <summary>  
        /// 创建VLC播放器  
        /// </summary>  
        /// <param name="libvlc_instance">VLC 全局变量</param>  
        /// <param name="handle">VLC MediaPlayer需要绑定显示的窗体句柄</param>  
        /// <returns></returns>  
        private libvlc_media_player_t Create_MediaPlayer(libvlc_instance_t libvlc_instance, IntPtr handle)
        {
            libvlc_media_player_t libvlc_media_player = IntPtr.Zero;

            try
            {
                if (libvlc_instance == IntPtr.Zero ||
                    libvlc_instance == null ||
                    handle == IntPtr.Zero ||
                    handle == null)
                {
                    return IntPtr.Zero;
                }

                //创建播放器  
                libvlc_media_player = SafeNativeMethods.libvlc_media_player_new(libvlc_instance);
                if (libvlc_media_player == null || libvlc_media_player == IntPtr.Zero)
                {
                    return IntPtr.Zero;
                }

                //设置播放窗口              
                SafeNativeMethods.libvlc_media_player_set_hwnd(libvlc_media_player, (int)handle);

                return libvlc_media_player;
            }
            catch
            {
                SafeNativeMethods.libvlc_media_player_release(libvlc_media_player);

                return IntPtr.Zero;
            }
        }

        /// <summary>  
        /// 释放媒体播放器  
        /// </summary>  
        /// <param name="libvlc_media_player">VLC MediaPlayer变量</param>  
        private void Release_MediaPlayer(libvlc_media_player_t libvlc_media_player)
        {
            try
            {
                if (libvlc_media_player != IntPtr.Zero ||
                    libvlc_media_player != null)
                {
                    if (SafeNativeMethods.libvlc_media_player_is_playing(libvlc_media_player))
                    {
                        SafeNativeMethods.libvlc_media_player_stop(libvlc_media_player);
                    }

                    SafeNativeMethods.libvlc_media_player_release(libvlc_media_player);
                }

                libvlc_media_player = IntPtr.Zero;
            }
            catch (Exception)
            {
                libvlc_media_player = IntPtr.Zero;
            }
        }

        /// <summary>  
        /// 播放网络媒体  
        /// </summary>  
        /// <param name="libvlc_instance">VLC 全局变量</param>  
        /// <param name="libvlc_media_player">VLC MediaPlayer变量</param>  
        /// <param name="url">网络视频URL，支持http、rtp、udp等格式的URL播放</param>  
        /// <returns></returns>  
        private bool NetWork_Media_Play(libvlc_instance_t libvlc_instance, libvlc_media_player_t libvlc_media_player, string url)
        {
            IntPtr pMrl = IntPtr.Zero;
            libvlc_media_t libvlc_media = IntPtr.Zero;

            try
            {
                if (url == null ||
                    libvlc_instance == IntPtr.Zero ||
                    libvlc_instance == null ||
                    libvlc_media_player == IntPtr.Zero ||
                    libvlc_media_player == null)
                {
                    return false;
                }

                pMrl = StrToIntPtr(url);
                if (pMrl == null || pMrl == IntPtr.Zero)
                {
                    return false;
                }

                //播放网络文件  
                libvlc_media = SafeNativeMethods.libvlc_media_new_location(libvlc_instance, pMrl);

                if (libvlc_media == null || libvlc_media == IntPtr.Zero)
                {
                    return false;
                }

                //将Media绑定到播放器上  
                SafeNativeMethods.libvlc_media_player_set_media(libvlc_media_player, libvlc_media);

                //释放libvlc_media资源  
                SafeNativeMethods.libvlc_media_release(libvlc_media);
                libvlc_media = IntPtr.Zero;

                if (0 != SafeNativeMethods.libvlc_media_player_play(libvlc_media_player))
                {
                    return false;
                }

                //休眠指定时间  
                Thread.Sleep(500);

                return true;
            }
            catch (Exception)
            {
                //释放libvlc_media资源  
                if (libvlc_media != IntPtr.Zero)
                {
                    SafeNativeMethods.libvlc_media_release(libvlc_media);
                }
                libvlc_media = IntPtr.Zero;

                return false;
            }
        }
        /// <summary>
        /// 获得视频时间
        /// </summary>
        /// <param name="libvlc_instance"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        private double Duration(libvlc_instance_t libvlc_instance, string url)
        {
            double duration_ = 0;
            libvlc_media_t libvlc_media = IntPtr.Zero;
            IntPtr pMrl = IntPtr.Zero;
            pMrl = StrToIntPtr(url);
            if (pMrl == null || pMrl == IntPtr.Zero)
            {
                return duration_;
            }
            libvlc_media = SafeNativeMethods.libvlc_media_new_path(libvlc_instance, pMrl);
            SafeNativeMethods.libvlc_media_parse(libvlc_media);
            duration_ = SafeNativeMethods.libvlc_media_get_duration(libvlc_media);
            return duration_;
        }

        /// <summary>
        /// 播放本地视频
        /// </summary>
        /// <param name="libvlc_instance"></param>
        /// <param name="libvlc_media_player"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        private bool Local_Media_Play(libvlc_instance_t libvlc_instance, libvlc_media_player_t libvlc_media_player, string url)
        {
            IntPtr pMrl = IntPtr.Zero;
            libvlc_media_t libvlc_media = IntPtr.Zero;

            try
            {
                if (url == null ||
                    libvlc_instance == IntPtr.Zero ||
                    libvlc_instance == null ||
                    libvlc_media_player == IntPtr.Zero ||
                    libvlc_media_player == null)
                {
                    return false;
                }

                pMrl = StrToIntPtr(url);
                if (pMrl == null || pMrl == IntPtr.Zero)
                {
                    return false;
                }

                //播放本地视频  
                libvlc_media = SafeNativeMethods.libvlc_media_new_path(libvlc_instance, pMrl);

                if (libvlc_media == null || libvlc_media == IntPtr.Zero)
                {
                    return false;
                }

                //将Media绑定到播放器上  
                SafeNativeMethods.libvlc_media_player_set_media(libvlc_media_player, libvlc_media);

                //释放libvlc_media资源  
                SafeNativeMethods.libvlc_media_release(libvlc_media);
                libvlc_media = IntPtr.Zero;

                if (0 != SafeNativeMethods.libvlc_media_player_play(libvlc_media_player))
                {
                    return false;
                }

                //休眠指定时间  
                Thread.Sleep(500);

                return true;
            }
            catch (Exception)
            {
                //释放libvlc_media资源  
                if (libvlc_media != IntPtr.Zero)
                {
                    SafeNativeMethods.libvlc_media_release(libvlc_media);
                }
                libvlc_media = IntPtr.Zero;

                return false;
            }
        }

        /// <summary>  
        /// 暂停或恢复视频  
        /// </summary>  
        /// <param name="libvlc_media_player">VLC MediaPlayer变量</param>  
        /// <returns></returns>  
        private bool MediaPlayer_Pause(libvlc_media_player_t libvlc_media_player)
        {
            try
            {
                if (libvlc_media_player == IntPtr.Zero ||
                    libvlc_media_player == null)
                {
                    return false;
                }

                if (SafeNativeMethods.libvlc_media_player_can_pause(libvlc_media_player))
                {
                    SafeNativeMethods.libvlc_media_player_pause(libvlc_media_player);

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>  
        /// 停止播放  
        /// </summary>  
        /// <param name="libvlc_media_player">VLC MediaPlayer变量</param>  
        /// <returns></returns>  
        private bool MediaPlayer_Stop(libvlc_media_player_t libvlc_media_player)
        {
            try
            {
                if (libvlc_media_player == IntPtr.Zero ||
                    libvlc_media_player == null)
                {
                    return false;
                }

                SafeNativeMethods.libvlc_media_player_stop(libvlc_media_player);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>  
        /// VLC MediaPlayer是否在播放  
        /// </summary>  
        /// <param name="libvlc_media_player">VLC MediaPlayer变量</param>  
        /// <returns></returns>  
        private bool MediaPlayer_IsPlaying(libvlc_media_player_t libvlc_media_player)
        {
            try
            {
                if (libvlc_media_player == IntPtr.Zero ||
                    libvlc_media_player == null)
                {
                    return false;
                }

                return SafeNativeMethods.libvlc_media_player_is_playing(libvlc_media_player);
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>  
        /// 录制快照  
        /// </summary>  
        /// <param name="libvlc_media_player">VLC MediaPlayer变量</param>  
        /// <param name="path">快照要存放的路径</param>  
        /// <param name="name">快照保存的文件名称</param>  
        /// <returns></returns>  
        private bool TakeSnapShot(libvlc_media_player_t libvlc_media_player, string path, string name)
        {
            try
            {
                string snap_shot_path = null;

                if (libvlc_media_player == IntPtr.Zero ||
                    libvlc_media_player == null)
                {
                    return false;
                }

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                snap_shot_path = path + "\\" + name;

                if (0 == SafeNativeMethods.libvlc_video_take_snapshot(libvlc_media_player, 0, snap_shot_path.ToCharArray(), 0, 0))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>  
        /// 获取信息  
        /// </summary>  
        /// <param name="libvlc_media_player"></param>  
        /// <returns></returns>  
        private bool GetMedia(libvlc_media_player_t libvlc_media_player)
        {
            libvlc_media_t media = IntPtr.Zero;

            try
            {
                if (libvlc_media_player == IntPtr.Zero ||
                    libvlc_media_player == null)
                {
                    return false;
                }

                media = SafeNativeMethods.libvlc_media_player_get_media(libvlc_media_player);
                if (media == IntPtr.Zero || media == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>  
        /// 获取已经显示的图片数  
        /// </summary>  
        /// <param name="libvlc_media_player"></param>  
        /// <returns></returns>  
        private int GetDisplayedPictures(libvlc_media_player_t libvlc_media_player)
        {
            libvlc_media_t media = IntPtr.Zero;
            libvlc_media_stats_t media_stats = new libvlc_media_stats_t();
            try
            {
                if (libvlc_media_player == IntPtr.Zero ||
                    libvlc_media_player == null)
                {
                    return 0;
                }

                media = SafeNativeMethods.libvlc_media_player_get_media(libvlc_media_player);
                if (media == IntPtr.Zero || media == null)
                {
                    return 0;
                }

                if (1 == SafeNativeMethods.libvlc_media_get_stats(media, ref media_stats))
                {
                    return media_stats.i_displayed_pictures;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>  
        /// 设置全屏  
        /// </summary>  
        /// <param name="libvlc_media_player"></param>  
        /// <param name="isFullScreen"></param>  
        private bool SetFullScreen(libvlc_media_player_t libvlc_media_player, int isFullScreen)
        {
            try
            {
                if (libvlc_media_player == IntPtr.Zero ||
                    libvlc_media_player == null)
                {
                    return false;
                }

                SafeNativeMethods.libvlc_set_fullscreen(libvlc_media_player, isFullScreen);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// 恢复播放
        /// </summary>
        private void Play(libvlc_media_player_t libvlc_mediaplayer)
        {
            if (libvlc_mediaplayer != IntPtr.Zero)
            {
                SafeNativeMethods.libvlc_media_player_play(libvlc_mediaplayer);
            }
        }
        //将string []转换为IntPtr  
        private static IntPtr StrToIntPtr(string[] args)
        {
            try
            {
                IntPtr ip_args = IntPtr.Zero;

                PointerToArrayOfPointerHelper argv = new PointerToArrayOfPointerHelper();
                argv.pointers = new IntPtr[11];

                for (int i = 0; i < args.Length; i++)
                {
                    argv.pointers[i] = Marshal.StringToHGlobalAnsi(args[i]);
                }

                int size = Marshal.SizeOf(typeof(PointerToArrayOfPointerHelper));
                ip_args = Marshal.AllocHGlobal(size);
                Marshal.StructureToPtr(argv, ip_args, false);

                return ip_args;
            }
            catch (Exception)
            {
                return IntPtr.Zero;
            }
        }

        //将string转换为IntPtr  
        private static IntPtr StrToIntPtr(string url)
        {
            try
            {
                if (string.IsNullOrEmpty(url))
                {
                    return IntPtr.Zero;
                }

                IntPtr pMrl = IntPtr.Zero;
                byte[] bytes = Encoding.UTF8.GetBytes(url);

                pMrl = Marshal.AllocHGlobal(bytes.Length + 1);
                Marshal.Copy(bytes, 0, pMrl, bytes.Length);
                Marshal.WriteByte(pMrl, bytes.Length, 0);

                return pMrl;
            }
            catch (Exception)
            {
                return IntPtr.Zero;
            }
        }
        #endregion

        #region 导入库函数
        [SuppressUnmanagedCodeSecurity]
        internal static class SafeNativeMethods
        {
            // 创建一个libvlc实例，它是引用计数的  
            [DllImport(@"libvlc.DLL", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
            internal static extern libvlc_instance_t libvlc_new(int argc, IntPtr argv);

            // 释放libvlc实例  
            [DllImport(@"libvlc.DLL", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
            internal static extern void libvlc_release(libvlc_instance_t libvlc_instance);

            //获取libvlc的版本  
            [DllImport(@"libvlc.DLL", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
            internal static extern String libvlc_get_version();

            //从视频来源(例如http、rtsp)构建一个libvlc_meida  
            [DllImport(@"libvlc.DLL", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
            internal static extern libvlc_media_t libvlc_media_new_location(libvlc_instance_t libvlc_instance, IntPtr path);

            //从本地文件路径构建一个libvlc_media  
            [DllImport(@"libvlc.DLL", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
            internal static extern libvlc_media_t libvlc_media_new_path(libvlc_instance_t libvlc_instance, IntPtr path);

            //释放libvlc_media  
            [DllImport(@"libvlc.DLL", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
            internal static extern void libvlc_media_release(libvlc_media_t libvlc_media_inst);

            // 创建一个空的播放器  
            [DllImport(@"libvlc.DLL", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
            internal static extern libvlc_media_player_t libvlc_media_player_new(libvlc_instance_t libvlc_instance);

            //从libvlc_media构建播放器  
            [DllImport(@"libvlc.DLL", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
            internal static extern libvlc_media_player_t libvlc_media_player_new_from_media(libvlc_media_t libvlc_media);

            //释放播放器资源  
            [DllImport(@"libvlc.DLL", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
            internal static extern void libvlc_media_player_release(libvlc_media_player_t libvlc_mediaplayer);

            // 将视频(libvlc_media)绑定到播放器上  
            [DllImport(@"libvlc.DLL", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
            internal static extern void libvlc_media_player_set_media(libvlc_media_player_t libvlc_media_player, libvlc_media_t libvlc_media);

            // 设置图像输出的窗口  
            [DllImport(@"libvlc.DLL", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
            internal static extern void libvlc_media_player_set_hwnd(libvlc_media_player_t libvlc_mediaplayer, Int32 drawable);

            //播放器播放  
            [DllImport(@"libvlc.DLL", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
            internal static extern int libvlc_media_player_play(libvlc_media_player_t libvlc_mediaplayer);

            //播放器暂停  
            [DllImport(@"libvlc.DLL", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
            internal static extern void libvlc_media_player_pause(libvlc_media_player_t libvlc_mediaplayer);

            //播放器停止  
            [DllImport(@"libvlc.DLL", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
            internal static extern void libvlc_media_player_stop(libvlc_media_player_t libvlc_mediaplayer);

            // 解析视频资源的媒体信息(如时长等)  
            [DllImport(@"libvlc.DLL", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
            internal static extern void libvlc_media_parse(libvlc_media_t libvlc_media);

            // 返回视频的时长(必须先调用libvlc_media_parse之后，该函数才会生效)  
            [DllImport(@"libvlc.DLL", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
            internal static extern Int64 libvlc_media_get_duration(libvlc_media_t libvlc_media);

            // 当前播放时间  
            [DllImport(@"libvlc.DLL", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
            internal static extern Int64 libvlc_media_player_get_time(libvlc_media_player_t libvlc_mediaplayer);

            // 设置播放时间  
            [DllImport(@"libvlc.DLL", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
            internal static extern void libvlc_media_player_set_time(libvlc_media_player_t libvlc_mediaplayer, Int64 time);

            // 获取音量  
            [DllImport(@"libvlc.DLL", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
            internal static extern int libvlc_audio_get_volume(libvlc_media_player_t libvlc_media_player);

            //设置音量  
            [DllImport(@"libvlc.DLL", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
            internal static extern void libvlc_audio_set_volume(libvlc_media_player_t libvlc_media_player, int volume);

            // 设置全屏  
            [DllImport(@"libvlc.DLL", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
            internal static extern void libvlc_set_fullscreen(libvlc_media_player_t libvlc_media_player, int isFullScreen);

            [DllImport(@"libvlc.DLL", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
            internal static extern int libvlc_get_fullscreen(libvlc_media_player_t libvlc_media_player);

            [DllImport(@"libvlc.DLL", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
            internal static extern void libvlc_toggle_fullscreen(libvlc_media_player_t libvlc_media_player);

            //判断播放时是否在播放  
            [DllImport(@"libvlc.DLL", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
            internal static extern bool libvlc_media_player_is_playing(libvlc_media_player_t libvlc_media_player);

            //判断播放时是否能够Seek  
            [DllImport(@"libvlc.DLL", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
            internal static extern bool libvlc_media_player_is_seekable(libvlc_media_player_t libvlc_media_player);

            //判断播放时是否能够Pause  
            [DllImport(@"libvlc.DLL", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
            internal static extern bool libvlc_media_player_can_pause(libvlc_media_player_t libvlc_media_player);

            //判断播放器是否可以播放  
            [DllImport(@"libvlc.DLL", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
            internal static extern int libvlc_media_player_will_play(libvlc_media_player_t libvlc_media_player);

            //进行快照  
            [DllImport(@"libvlc.DLL", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
            internal static extern int libvlc_video_take_snapshot(libvlc_media_player_t libvlc_media_player, int num, char[] filepath, int i_width, int i_height);

            //获取Media信息  
            [DllImport(@"libvlc.DLL", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
            internal static extern libvlc_media_t libvlc_media_player_get_media(libvlc_media_player_t libvlc_media_player);

            //获取媒体信息  
            [DllImport(@"libvlc.DLL", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
            internal static extern int libvlc_media_get_stats(libvlc_media_t libvlc_media, ref libvlc_media_stats_t lib_vlc_media_stats);
            [DllImport(@"libvlc.DLL", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
            internal static extern void libvlc_video_set_aspect_ratio(libvlc_media_player_t libvlc_media_player, char[] aspect);
        }
        #endregion
    }
}