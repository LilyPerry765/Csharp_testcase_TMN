   M S B A M L   `   `   `  ���� S  OEricssonAlarmService, Version=2.0.0.19579, Culture=neutral, PublicKeyToken=nullclr-namespace:TMNTMN      TMN.SettingsWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     cclr-namespace:TMN     $��Tahoma)�  � 
ResizeMode$  NoResize=� � WindowStartupLocation$ CenterScreen=� � Title$ Ericsson Service Settings��$	��288��$	��368��.��]� $��RightToLeft=�.+���  �� Orientation$ 
Horizontal=�$��Bottom=�.+��� -   $��
saveButton��$��3q� �� 	IsDefault . $��ثبت��$��23��$��75���� -   $��cancelButton��$��3q�$��خروج�� �� IsCancel . $��23��$��75���� $��5q�.+��� .��مرکز جاری:�� -    ��Name$ centersCombo��$	��151��$
��Left=� �� DisplayMemberPath$ DisplayName�� �� SelectedValuePath$ ID���� $	��0,5q�.��]� .+��� $
��Left=�.+��� .��روش اتصال:�� -   $ connectionComboBox��$��LeftToRight=�$	��151��$
��Left=�.@��� -   	 ��Name$	 telnetComboBoxItem��$��TCP/IP (telnet)���� -   $	 serialPortComboboxItem��$��RS 232 (serial port)��� $��LeftToRight=�
 � 	Resources
  �� 
TargetType �� Property �� Value��%a   '��       �� " ��.���  � ��$	 100����  � ��$
 Left=���  � ��$ 5,0,5,5q�.+��� -    ��Name$ serialSettingsPanel��$y�Hidden=�.+��� .��
Port Name:�� -    ��Name$ 
comTextBox���� -	   $ telnetSettingsPanel��.+��� .��
Seitch IP:�� -
   $ 	IpTextBox���� .��Switch User Code:�� -   $ userCodeTextbox���� .��Switch Password:�� -   $ passwordTextbox��