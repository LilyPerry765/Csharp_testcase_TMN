   M S B A M L   `   `   `  ���� I  ETMN.Common, Version=2.0.1.19574, Culture=neutral, PublicKeyToken=null;,clr-namespace:TMN.Assets;assembly=TMN.Common
TMN.Assets  M ITMN.Converters, Version=1.1.0.19574, Culture=neutral, PublicKeyToken=nullG4clr-namespace:TMN.Converters;assembly=TMN.ConvertersTMN.Converters U QEnterprise.Wpf, Version=1.1.0.0, Culture=neutral, PublicKeyToken=80f1ae88e0a8b4d1G4clr-namespace:Enterprise.Wpf;assembly=Enterprise.WpfEnterprise.Wpf B >TMN, Version=2.2.0.19582, Culture=neutral, PublicKeyToken=null1clr-namespace:TMN.Views.ListsTMN.Views.Lists )   #TMN.Views.Lists.LongRecordsListView   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     :a,clr-namespace:TMN.Assets;assembly=TMN.Common     Econv4clr-namespace:TMN.Converters;assembly=TMN.Converters     Br4clr-namespace:Enterprise.Wpf;assembly=Enterprise.Wpf     +vclr-namespace:TMN.Views.Lists     #  TMN.Views.Lists.ItemsListBase    ListView  ��  �� View �� .P��� $��نام کشيک�� �� Width$
 Auto�� �� DisplayMemberBinding �� *User.FullName+�� $��تاريخ ميلادی��$
 Auto�� �� *Date+ �� StringFormat$ 
yyyy/MM/dd���� $��تاريخ شمسی��$
 Auto�� �� *Date+)  #TMN.Converters.PersianDateConverter   Instance �� 	Converter# Z  �� ConverterParameter$ d���� $��ساعت��$
 Auto�� �� *Date+$ HH:mm���� $ ��نام مسير ورودی��$
 Auto�� �� *Route.RouteName+�� $��شماره مشترک A��$
 Auto�� �� *	ANumber+�� $��شماره مشترک B��$
 Auto�� �� *	BNumber+�� $��شماره کانال��$
 Auto�� �� *	Channel+G4clr-namespace:TMN.Converters;assembly=TMN.ConvertersTMN.Converters �� Econv4clr-namespace:TMN.Converters;assembly=TMN.Converters     $��وضعيت کانال��$
 Auto�� �� *State++  %TMN.Converters.ChannelStatesConverter   Instance# Z �� $��مدت مکالمه��$
 Auto�� �� *Length+�� $��توضيحات��$
 Auto�� �� *Description+