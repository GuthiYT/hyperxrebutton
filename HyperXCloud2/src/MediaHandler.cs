using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace HyperXCloud2
{
    public static class MediaHandler
    {
        // THESE CAN BE SWAPED TO DIFFERENT KEYCODES BY USER TO HAVE A DIFFERENT FUNCTION!!!
        public static byte NEXT = 0xB0;// keycode to jump to next track
        public static byte PLAY_PAUSE = 0xB3;// keycode to play or pause a song
        public static byte PREV = 0xB1;// keycode to jump to prev track

        public static int VOLUME_CURRENT = 0;
        public static int VOLUME_AMOUNT = 5; // DEFAULT CHANGES VOLUME BY +-2


        [DllImport("user32.dll")]
        public static extern void keybd_event(byte virtualKey, byte scanCode, uint flags, IntPtr extraInfo);
        public static void PauseResumeAudio()
        {
            keybd_event(PLAY_PAUSE, 0, 1, IntPtr.Zero);
        }
        public static void NextAudio()
        {
            keybd_event(NEXT, 0, 1, IntPtr.Zero);
        }
        public static void PrevAudio()
        {
            keybd_event(PREV, 0, 1, IntPtr.Zero);
        }

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessageW(IntPtr hWnd, int Msg,IntPtr wParam, IntPtr lParam);
        public static void VolumeUp()
        {

            int wanted = Clamp(VOLUME_CURRENT + VOLUME_AMOUNT, 0, 100);
            VOLUME_CURRENT = wanted;

            if (VOLUME_AMOUNT == 2) return;

            VideoPlayerController.AudioManager.SetMasterVolume(wanted);
        }
        public static void VolumeDown()
        {
            int wanted = Clamp(VOLUME_CURRENT - VOLUME_AMOUNT, 0, 100);
            VOLUME_CURRENT = wanted;

            if (VOLUME_AMOUNT == 2) return;

            VideoPlayerController.AudioManager.SetMasterVolume(wanted);
        }

        private static int Clamp(int value, int min, int max)
        {
            if(value > max)
            {
                return max;
            } else if(value < min){
                return min;
            } else
            {
                return value;
            }
        }
    }
}
