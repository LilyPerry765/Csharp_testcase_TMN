   M S B A M L   `   `   `  ���� I  ETMN.Common, Version=2.0.1.19574, Culture=neutral, PublicKeyToken=null;,clr-namespace:TMN.Assets;assembly=TMN.Common
TMN.Assets  M ITMN.Converters, Version=1.1.0.19574, Culture=neutral, PublicKeyToken=nullG4clr-namespace:TMN.Converters;assembly=TMN.ConvertersTMN.Converters U QEnterprise.Wpf, Version=1.1.0.0, Culture=neutral, PublicKeyToken=80f1ae88e0a8b4d1G4clr-namespace:Enterprise.Wpf;assembly=Enterprise.WpfEnterprise.Wpf B >TMN, Version=2.2.0.19582, Culture=neutral, PublicKeyToken=null    TMN.Views.Details.CardView   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     :a,clr-namespace:TMN.Assets;assembly=TMN.Common     Econv4clr-namespace:TMN.Converters;assembly=TMN.Converters     Br4clr-namespace:Enterprise.Wpf;assembly=Enterprise.Wpf     $	��130��.����   �� Orientation$  
Horizontal=�.+��� .+��� $��Right=� �� Target ��  �� ElementName$ cmbType��.��نوع كارت:�� -    ��Name$ lblSlot��$��Right=� �� $ 	nudSlotNo��.��شماره اسلات:�� $��Right=� �� $ txtName��.��	نام:�� $��Right=� �� $ txtSerialNo��.��شماره سريال:�� $��Right=� � ToolTip$+ %آدرس نرم افزاری کارت�� �� $ 
EQPTextBox��.��EQP:�� .+�3clr-namespace:TMN.UserControlsTMN.UserControls % TMN.UserControls.EntityComboBox  -ucclr-namespace:TMN.UserControls     -     Name$ cmbType��%  TMN.UserControls.EntityComboBox   DropDownClosed$ cmbType_DropDownClosed��$	��140�� �� DisplayMemberPath$
 Name�� �� SelectedValuePath$ ID��$��0,2,0,2q�$
��Left=�	 �� SelectedItem	 �� *
CardType+�� $  
Horizontal=�$��0,2,0,2q�.+�"  Enterprise.Wpf.NumericUpDown  -   
  Name$
 	nudSlotNo��   Minimum$ 1X�$
��Left=�   Value ��  �� Path$ SlotNo� �� Mode$ TwoWay=��� $��16��$	��5,2q�$) #اين اسلات اشغال است����%   TMN.Assets.ImageSourceExtension     Path$ warning.png��y��� *IsOnBusySlot+/  )TMN.Converters.VisibilityBooleanConverter   Instance �� 	Converter# Z �� -    ��Name$ txtName��$	��140��$��0,2,0,2q�$
��Left=����� *Name+�� -   $ txtSerialNo��$	��140��$��0,2,0,2q�$
��Left=����� *
SerialNo+�� -   $ 
EQPTextBox��$+ %آدرس نرم افزاری کارت��$	��140��$��0,2,0,2q�$
��Left=����� *EQP+