   M S B A M L   `   `   `  ���� B  >TMN, Version=2.2.0.19582, Culture=neutral, PublicKeyToken=null3clr-namespace:TMN.UserControlsTMN.UserControls  U QEnterprise.Wpf, Version=1.1.0.0, Culture=neutral, PublicKeyToken=80f1ae88e0a8b4d1G4clr-namespace:Enterprise.Wpf;assembly=Enterprise.WpfEnterprise.Wpf #    TMN.Views.Details.ChannelView   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -ucclr-namespace:TMN.UserControls     Br4clr-namespace:Enterprise.Wpf;assembly=Enterprise.Wpf     .���� -     �� Orientation$  
Horizontal=�.+��� .+��� $��Right=� �� Target ��  �� ElementName$ RouteComboBox��.��	مسير:�� $��Right=� �� $ TimeSlotNumericUpDown��.��تايم اسلات:�� $��Right=� �� $ ChannelNumericUpDown��.��کانال:�� $��Right=� �� $ 	CICUpDown��.��CIC:�� .+�%  TMN.UserControls.EntityComboBox  -     Name$ RouteComboBox��$	��140�� �� 
IsEditable .  �� DisplayMemberPath$
 TGNO�� �� SelectedValuePath$ ID��$��0,2,0,2q�$
��Left=� �� SelectedValue �� *	RouteID+"  Enterprise.Wpf.NumericUpDown  -     Name$ TimeSlotNumericUpDown��$��0,2,0,2q�$
��Left=�	   Value	 �� 
 �� Path$
 TimeSlot� �� Mode$ TwoWay=�  -   $ ChannelNumericUpDown��$��0,2,0,2q�$
��Left=�	 �� $	
 LNO�$ TwoWay=��� $  
Horizontal=�.+��� ���� *
TimeSlot+�� $��Center=�-  -   $ 	CICUpDown��$��0,2,0,2q�$
��Left=�	 �� $
 Link.CIC�$ TwoWay=�