   M S B A M L   `   `   `  ���� M  ITMN.Converters, Version=1.1.0.19574, Culture=neutral, PublicKeyToken=nullG4clr-namespace:TMN.Converters;assembly=TMN.ConvertersTMN.Converters  I ETMN.Common, Version=2.0.1.19574, Culture=neutral, PublicKeyToken=null;,clr-namespace:TMN.Assets;assembly=TMN.Common
TMN.Assets B >TMN, Version=2.2.0.19582, Culture=neutral, PublicKeyToken=null1clr-namespace:TMN.Views.ListsTMN.Views.Lists $   TMN.Views.Lists.EventsListView   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Econv4clr-namespace:TMN.Converters;assembly=TMN.Converters     :a,clr-namespace:TMN.Assets;assembly=TMN.Common     +vclr-namespace:TMN.Views.Lists     #  TMN.Views.Lists.ItemsListBase    Adding$  ItemsListView_Adding��#  TMN.Views.Lists.ItemsListBase   ListView ��  �� View �� .P���  �� Width$ 25�� �� CellTemplate �� .R��� $��16����%  TMN.Assets.ImageSourceExtension     Path$ lock.png��y��� *
IsLocked+/   )TMN.Converters.VisibilityBooleanConverter   Instance �� 	Converter# Z  �� ConverterParameter$ d���� $'��!نام و نام خانوادگی��$
 Auto��	 �� DisplayMemberBinding	 �� *User.FullName+�� $��تاريخ شمسی��$
 Auto��	 �� *Time+)   #TMN.Converters.PersianDateConverter
   Instance# Z
 $ d���� $��تاريخ ميلادی��$
 Auto��	 �� *Time+ �� StringFormat$ 
yyyy/MM/dd���� $��زمان��$
 Auto��	 �� *Time+$ HH:mm���� $��نوع عمليات��$
 Auto��	 �� *EventType.Name+�� $��شرح عمليات��$
 Auto��	 �� *	Comment+