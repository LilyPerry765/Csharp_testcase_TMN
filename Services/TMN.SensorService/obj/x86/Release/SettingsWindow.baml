   M S B A M L   `   `   `  ���� L  HSensorService, Version=2.2.0.19575, Culture=neutral, PublicKeyToken=nullclr-namespace:TMNTMN      TMN.SettingsWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     cclr-namespace:TMN     $��Tahoma)�  � 
ResizeMode$  NoResize=� � WindowStartupLocation$ CenterScreen=� � Title$ Sensor Service Settings��$	��326��$	��486��.��]� .+��� $��Bottom=�.@��� -    ��Name$ statusTextblock���� -   $��RightToLeft=�.@��� $��تنظيمات��.��]� $��RightToLeft=�.+���  �� Orientation$ 
Horizontal=�$��Bottom=�.+��� -   $��
saveButton��$��3q� �� 	IsDefault . $��ثبت��$��23��$��75���� -   $��cancelButton��$��3q�$��خروج�� �� IsCancel . $��23��$��75���� -    ��Name$ sensorSettingsButton��$
��20,3q�$��Right=�$��23��.��-+تعريف سالن ها و سنسور ها�� $��5q�.+��� .��مرکز جاری:�� -    ��Name$ centersCombo��$	��151��$
��Left=�	 �� DisplayMemberPath$	 DisplayName��
 �� SelectedValuePath$
 ID���� .��شماره پورت:�� -    ��Name$ 
comTextBox��$��5q�$
��Left=�$	��100��$��RightToLeft=��� .��.,فاصله زمانی (ميلی ثانیه):� .+��� -   $ sensorIntervalTextBox��$��50��$��3q��� -	    ��Name$ buzzerCheckbox��$��5q�.��آلارم دستگاه�� -
    ��Name$ calibrasionTabItem��$��کاليبراسيون��.���� .+��� 
�������$
��0,10q�.����با کليک روی هر کدام از نمايشگر ها می توانيد سنسور مربوطه را کاليبره کنيد.A�  A� Rows$ 1��$
��5,20q�$��Center=�$��LeftToRight=�.+�   TMN.SensorLedButton  -     Name$ temp1��   TMN.SensorLedButton   Click$ SensorLed_Click��   Title$ Temperature 1��   SensorNumber$ 1��  -   $ temp2��$ SensorLed_Click��$ Temperature 2��$ 2��  -   $ temp3��$ SensorLed_Click��$ Temperature 3��$ 3��  -   $ humidity��$ SensorLed_Click��$ Humidity��$ 4��   DisplayBrush
 �2�2�