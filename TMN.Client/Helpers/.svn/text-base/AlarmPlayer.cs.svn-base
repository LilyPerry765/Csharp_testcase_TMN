using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Media;
using Enterprise;
using System.IO;

namespace TMN
{
    public static class AlarmPlayer
    {
        private static SoundPlayer player = new SoundPlayer();
        public static bool isPlaying = false;
        public static SoundAlarmSeverities CurrentSeverity = SoundAlarmSeverities.None;

        public static void Play(SoundAlarmSeverities severity, string application)
        {
            try
            {
                if ((CurrentSeverity != severity || !isPlaying))
                {
                    string path = "";
                    switch (severity)
                    {
                        case SoundAlarmSeverities.Information:
                            path += Setting.Get(Setting.SOUND_ALARM_DC, Setting.DEFAULT_SOUND_ALARM_DC);
                            break;
                        case SoundAlarmSeverities.Critical:
                            path += Setting.Get(Setting.SOUND_ALARM_CRITICAL, Setting.DEFAULT_SOUND_ALARM_CRITICAL);
                            break;
                        case SoundAlarmSeverities.Major:
                            path += Setting.Get(Setting.SOUND_ALARM_MAJOR, Setting.DEFAULT_SOUND_ALARM_MAJOR);
                            break;
                        case SoundAlarmSeverities.Minor:
                            path += Setting.Get(Setting.SOUND_ALARM_MINOR, Setting.DEFAULT_SOUND_ALARM_MINOR);
                            break;
                        case SoundAlarmSeverities.Power:
                            //path += Setting.Get(Setting.SOUND_ALARM_CRITICAL, Setting.DEFAULT_SOUND_ALARM_CRITICAL);
                            path += Setting.Get(Setting.SOUND_ALARM_POWER, Setting.DEFAULT_SOUND_ALARM_POWER);
                            break;
                        case SoundAlarmSeverities.Cable:
                            path += Setting.Get(Setting.SOUND_ALARM_CRITICAL, Setting.DEFAULT_SOUND_ALARM_CRITICAL);
                            //path += Setting.Get(Setting.SOUND_ALARM_CABLE, Setting.DEFAULT_SOUND_ALARM_CABLE);
                            break;
                        case SoundAlarmSeverities.Sensor:
                            path += Setting.Get(Setting.SOUND_ALARM_CRITICAL, Setting.DEFAULT_SOUND_ALARM_CRITICAL);
                            //path += Setting.Get(Setting.SOUND_ALARM_CABLE, Setting.DEFAULT_SOUND_ALARM_CABLE);
                            break;
                        case SoundAlarmSeverities.None:
                            Stop(application);
                            return;
                        default:
                            Logger.WriteError("Invalid severity \"{0}\" for playing sound.", severity);
                            return;
                    }

                    player.Dispose();
                    player = new SoundPlayer();
                    Stream sound = new FileStream(path, FileMode.Open, FileAccess.Read);
                    sound.Position = 0;     // Manually rewind stream 
                    player.Stream = sound;
                    CurrentSeverity = severity;
                    playerApplication = application;
                    player.PlayLooping();
                    isPlaying = true;

                }
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
            }
        }

        public static string playerApplication = "";
        public static void Stop(string application)
        {
            if (playerApplication == application)
            {
                player.Stop();
                isPlaying = false;
            }
        }

        //public static void SetActiveApplication(string application)
        //{
        //    playerApplication = application;
        //}
    }
}
