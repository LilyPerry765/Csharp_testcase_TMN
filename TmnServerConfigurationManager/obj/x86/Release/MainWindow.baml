   M S B A M L   `   `   `  ����    Server Configuration Manager    TMN.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     $��Tahoma)�  � Title$&   TMN Server Configuration Manager��$	��357��$	��441�� � 	Resources  �� 
TargetType   SmallTextbox �� Property �� Value��%Z   &	        �� " ��.���  � ��$ 3q���  � ��$ 50����  � ��$
 Left=�.��]� .+��� $��Bottom=� �� Orientation$ 
Horizontal=�$��RightToLeft=� ��%B   '��       �� " ��.���  � ��$ 70����  � ��$ 3q�.+��� -    ��Name$ okButton��.��Save�� $��5q�.@��� $��Server Security��.���� .+��� 
�������$
��Wrap=�usThese information is required to authenticate services who want to access shared resources of this server remotely.�� $ Vertical=�$��5q�$	��200��$
��Left=�.+��� .��Server username:�� -    ��Name$ usernameTextBox���� .��Current password:�� -    ��Name$ currentPassword��$��1q�
�������$��23���� .��New password:�� -   $ passwordTextBox���� $��Sensor Service��.����  	 �� BasedOn��%   '��       7   �� 8	   " ��.+��� 
�������.��-+Leave ecah box blanck to use default value.�� 
 � ToolTip$T
 NSensor data will be queried but not saved every ? seconds just for monitoring.��.��Query interval (seconds):�� -   $  sensorQueryIntervalTextbox���� $<
 6Sensor data will be saved every ? seconds for logging.��.��Save interval (seconds):�� -   $ sensorSaveIntervalTextbox���� $p
 jSensor Service will be considered as disconnected if no activity is reported within the following timeout.��.��Activity timeout (seconds):�� -   $" sensorActivityTimeoutTextbox���� .��Delete old data after (Days):�� -   $ sensorExpireTextbox���� $��Alarm Services��.����  ��%   '��       7   �� 8	   " ��.+��� 
�������.��-+Leave ecah box blanck to use default value.�� .��Query interval (seconds):�� -	   $ alarmQueryIntervalTextbox���� $n
 hAlarm Service will be considered as disconnected if no activity is reported within the following timeout��.��Activity timeout (seconds):�� -
   $! alarmActivityTimeoutTextbox���� $&�� Delete old data after (Seconds):���� -   $��alarmExpireTextBox���� $��Alarm Panel��.����  ��%   '��       7   �� 8	   " ��.+��� 
�������.��-+Leave ecah box blanck to use default value.�� .��0.Center alarm panel refresh interval (seconds):�� -   $  centerPanelIntervalTextbox���� .��20Regional alarm panel refresh interval (seconds):�� -   $  regionPanelIntervalTextbox���� $+��%Voice Alarm Interval (0 = continous):���� 
�������$
��Wrap=�1/{ X * regional or center alarm panel interval }�� -   $��VoiceAlarmIntervalTextbox��