   M S B A M L   `   `   `  ���� B  >TMN, Version=2.2.0.19582, Culture=neutral, PublicKeyToken=null3clr-namespace:TMN.UserControlsTMN.UserControls  I ETMN.Common, Version=2.0.1.19574, Culture=neutral, PublicKeyToken=null;,clr-namespace:TMN.Assets;assembly=TMN.Common
TMN.Assets %    TMN.UI.Windows.AlarmPanelWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -ucclr-namespace:TMN.UserControls     :a,clr-namespace:TMN.Assets;assembly=TMN.Common     $��Tahoma)�$��RightToLeft=�  � Title$  آلارم پنل�� � Topmost . $	��650��   backgroundPicture#��[  $
��1145��.���� $
��Auto=�$
��Auto=�.���� $��LeftToRight=�$��Center=�$��Center=� �� CornerRadius$ 5�����	#30FFFFFF$��1q�
����d�._��� $ 5��$��1q�
�������._�� $��5q� � 	Resources  �� 
TargetType �� Property �� Value"   TMN.UserControls.BlinkingLed3  -System.Windows.Media.Effects.DropShadowEffect   ShadowDepth   
BlurRadius	   Color
   Opacity��%�  '��       '�� �     '  �     '�� Y    �� " ��.���  � ��$ 5q���  � ��$	 240����  � ��$	 300����  � ��
 ���� ��  � ��
 ��d��� " ��.���  �@��TextAlignment$ Center=���  � ��$ Center=���  � ��$ 15(��� "  .���  � ��$ 3,0,0,0q���  �@�ToolTip$ Click To View Log����  �@�Cursor$
 Hand���� " ��.���  �@J�Effect   $ 1Q�$ 1Q�$	 White��$
 1Q� � RowDefinitions �� -    ��Name$ 	headerRow���� -   $ 	sensorRow���� -   $ alarmPanelRow��.+�� -    �Name$ 
HeaderGrid��$��0�� � ColumnDefinitions �� $
��Auto ��� �� $
��Auto � �� �� .+��� -    ��Name$ CenterNameLabel��$��RightToLeft=�$��Center=�$��Center=�$��	ExtraBold"�$��	B Nazanin)�$��0q�$��30(�
���   �$0��*محل نمايش نام مرکز جاری��$��1���� $��1��$��1��$��Center=� �� Orientation$ 
Horizontal=� ��%D   '��       �� " ��.���  �@J�Effect   $ 0Q�$
 1Q�.+��� -   $ DateTimeLabel��$��LeftToRight=�$��Center=�$��Center=�$
��Bold"�$��Tahoma)�$��20(�$%��Christian Date Time Placeholder��
��������� -   $ PersianDateTimeLabel��$��RightToLeft=�$��Center=�$��Center=�$
��Bold"�$��	B Nazanin)�$��25(�$5��/محل ساعت و تاريخ هجری شمسی��
�������  -     Name$ alarmServiceLed��"   TMN.UserControls.BlinkingLed   PreviewMouseUp$$ alarmServiceLed_PreviewMouseUp��$��0��$��2��$��35��$	��100��$��3q�   Title$ Service��   InnerBackground
 �2�2�  -	   $ PoweralarmLed��$��2��$��1��$��35�� � ToolTip$  ��$	��100��$��3q�$ Power�� � Cursor$ Arrow��
 �2�2�� $��3q�$��0��$��0�� �� $
��Auto ��� .+��� $��1��$
��Left=�$��5q�Mute All Alarms�� $ 3��$��40��$��.5q�
���   �$
��Left=�._�"   TMN.UserControls.SoundButton  -
     Name$ masterSound��"   TMN.UserControls.SoundButton   IsMuteChanged$ Sound_IsMuteChanged��$	��5,0q�$" کنترل مشترک صدا��� $��3q�$��1��$��0�� �� $
��Auto ��� .+��� $��1��$
��Left=�$��5q�اتصال به مرکز�� -    ��Name$ remoteButton��$
��Left=�$��0q�$��40��
������ .���� $��30����%  TMN.Assets.ImageSourceExtension  *
remote.png+A� -    A�Name$ sensorsPanel�� A� Rows$ 1��$��1��
������ ��Z� .+��� $��70��+)This Panel is used for populating sensors�� $��2��$��1q�
����d����	#FFB8D9F3$ 5��._��� 
�������$��1q� �� Padding$	 0,5q�$ 4��._�� -   $ AlarmPanelGrid�� �� $��20 ��� $
��Auto ��� $��* � �� $
��Auto ��� $	��10* ��� $	��10* ��� $	��10* ��� $��2* �.+��� $��Critical Alarms��$��1���� $��2��$��Major Alarms���� $��3��$��Minor Alarms���� $��4��$	��INF��� $��5q�$��1��$��1�� �� $
��Auto ��� .+�  -   $ criticalLed��   MouseUp$ criticalLed_MouseUp��   DisplayModeChanged$ Led_DisplayModeChanged��
 �  ��$��1��  -   $ 	critSound��$ Sound_IsMuteChanged��$
��Left=�� $��2��$��5q�$��1�� �� $
��Auto ��� .+�  -   $ majorLed��$ majorLed_MouseUp��$ Led_DisplayModeChanged��
 � E��$��1��  -   $ 
majorSound��$ Sound_IsMuteChanged��� $��3��$��5q�$��1�� �� $
��Auto ��� .+�  -   $ minorLed��$ minorLed_MouseUp��$ Led_DisplayModeChanged��
 � ���$��1��  -   $ 
minorSound��$ Sound_IsMuteChanged��� $��4��$��5q�$��1�� �� $��* ��� -    ��Name$ newCriticalAlarmsList��$��2��$��1��$	��270���� -   $ newMajorAlarmsList��$��2��$��2��$	��270���� -   $ newMinorAlarmsList��$��3��$��2��$	��270��  -   $ infoLed��$ infoLed_MouseUp��$ Led_DisplayModeChanged��
 ��d�$��40��$��4��$��5q�$��2��  � OverridesDefaultStyle  .  ! � DataContext! �� 