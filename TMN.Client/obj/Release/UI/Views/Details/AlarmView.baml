   M S B A M L   `   `   `  ���� U  QEnterprise.Wpf, Version=1.1.0.0, Culture=neutral, PublicKeyToken=80f1ae88e0a8b4d1G4clr-namespace:Enterprise.Wpf;assembly=Enterprise.WpfEnterprise.Wpf  O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem M ITMN.Converters, Version=1.1.0.19574, Culture=neutral, PublicKeyToken=nullG4clr-namespace:TMN.Converters;assembly=TMN.ConvertersTMN.Converters B >TMN, Version=2.2.0.19582, Culture=neutral, PublicKeyToken=null3clr-namespace:TMN.UserControlsTMN.UserControls !   TMN.Views.Details.AlarmView   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Br4clr-namespace:Enterprise.Wpf;assembly=Enterprise.Wpf     6sys&clr-namespace:System;assembly=mscorlib     Econv4clr-namespace:TMN.Converters;assembly=TMN.Converters     -ucclr-namespace:TMN.UserControls     $	��326��$	��245��.����   �� Orientation$  
Horizontal=�.+��� .+��� $��Right=� �� Target ��  �� ElementName$ RouteComboBox��.��	مسير:�� $��Right=� �� $ FailedLinkComboBox��.��	لينک:�� $��Right=� �� $ AlarmTypeComboBox��.��نوع آلارم:�� $��Right=� �� $" DisconnectTimePersianDateBox��.��زمان شروع:�� $��Right=� �� $ ConnectTimePersianDateBox��.��زمان رفع:�� $��Right=� �� $  AnnounceDatePersianDateBox��.��تاريخ اعلام:�� $��Right=� �� $ ReportReceiverTextBox��.��گيرنده گزارش:�� $��Right=� �� $ FixerTextBox��.��رفع کننده:�� $��Right=� �� $ AssessorTextBox��.��تحويل گيرنده:�� $��Right=�.��علت خرابی:�� $��Right=� �� $ DescriptionTextBox��.��توضيحات:�� $	��166��.+�% TMN.UserControls.EntityComboBox  -     Name$ RouteComboBox��%  TMN.UserControls.EntityComboBox   SelectionChanged$$ RouteComboBox_SelectionChanged��$	��160�� �� 
IsEditable .  �� DisplayMemberPath$
 TGNO�� �� SelectedValuePath$ ID��$��0,2,0,2q�$
��Left=� �� SelectedItem �� *Route+�� -   	 ��Name$	 FailedLinkComboBox�� . $	��160��$ Address��$ ID��$��0,2,0,2q�$
��Left=� �� *Link+  -   $ AlarmTypeComboBox��$	��160��$
 Name��$ ID��$��0,2,0,2q�$
��Left=� �� *	AlarmType+#   Enterprise.Wpf.PersianDateBox  -   
  Name$"
 DisconnectTimePersianDateBox��$	��160��   DisplayMode$ DateTime=�   
ShowSecond .  $��0,2,0,2q�$
��Left=�   Date �� *DisconnectTime+  -   $
 ConnectTimePersianDateBox��$	��160��$ DateTime=� .  $��0,2,0,2q�$
��Left=� �� *ConnectTime+  -   $ 
 AnnounceDatePersianDateBox��$	��160��$
 Date=�$��0,2,0,2q�$
��Left=� �� *AnnounceDate+�� -    ��Name$ ReportReceiverTextBox��$	��160��$��0,2,0,2q�$
��Left=����� *ReportReceiver+�� -   $ FixerTextBox��$	��160��$��0,2,0,2q�$
��Left=����� *Fixer+�� -	   $ AssessorTextBox��$	��160��$��0,2,0,2q�$
��Left=����� *
Assessor+�� -
   $ DamagePlaceTextBox��$	��160��$��0,2,0,2q�$
��Left=����� *DamagePlace+�� -   $ DescriptionTextBox��$	��160��$��0,2,0,2q�$
��Left=�$��46������ *Description+