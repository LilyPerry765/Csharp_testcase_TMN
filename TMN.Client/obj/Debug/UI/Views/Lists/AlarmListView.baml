   M S B A M L   `   `   `  ����M  ITMN.Converters, Version=1.1.0.26823, Culture=neutral, PublicKeyToken=nullG4clr-namespace:TMN.Converters;assembly=TMN.ConvertersTMN.Converters  5      I ETMN.Common, Version=2.0.1.40989, Culture=neutral, PublicKeyToken=null;,clr-namespace:TMN.Assets;assembly=TMN.Common
TMN.Assets 5      B >TMN, Version=2.8.0.37957, Culture=neutral, PublicKeyToken=null1clr-namespace:TMN.Views.ListsTMN.Views.Lists 5      $   TMN.Views.Lists.AlarmsListView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Econv4clr-namespace:TMN.Converters;assembly=TMN.Converters     5      :a,clr-namespace:TMN.Assets;assembly=TMN.Common     5      +vclr-namespace:TMN.Views.Lists     5      #  TMN.Views.Lists.ItemsListBase    ListView  5      �� 5	   
    �� View 5
      �� 5      .P�5      �� $��زمان ثبت��6%    �� Width$
 Auto��5   %    �� DisplayMemberBinding 5   %   �� *
ReportTime+)   #TMN.Converters.PersianDateConverter   Instance �� 	Converter# Z  �� ConverterParameter$ dt��5   %   �� 5      $��مرکز مقابل��6%   $
 Auto��5   %    5   %   �� *Route.Destination.Name+5   %   �� 5      $��ادرس لينک��6%   $
 Auto��5   %    5   %   �� *Link.Address+5   %   �� 5      $��گزارش کننده��6%   $
 Auto��5   %    5   %   �� *User.FullName+5   %   �� 5      $��گيرنده گزارش��6%   $
 Auto��5   %    5   %   �� *ReportReceiver+5   %   �� 5      $��تاريخ اعلام��6%   $
 Auto��5   %    5   %   �� *AnnounceDate+# Z $ d��5   %   �� 5      $��نوع آلارم��6%   $
 Auto��5    %    5   %   �� *AlarmType.Name+5    %   �� 5!      $��علت خرابی��6%   $
 Auto��5#   %    5"   %   �� *DamagePlace+5#   %   �� 5$      $��زمان قطع��6%   $
 Auto��5&   %    5%   %   �� *DisconnectTime+# Z $ dt��5&   %   �� 5'      $��زمان وصل��6%   $
 Auto��5)   %    5(   %   �� *ConnectTime+# Z $ dt��5)   %   �� 5*      $��مدت قطع��6%   $
 Auto��5,   %    5+   %   �� *
Duration+5,   %   �� 5-      $��رفع کننده��6%   $
 Auto��5/   %    5.   %   �� *Fixer+5/   %   �� 50      $��تحويل گيرنده��6%   $
 Auto��52   %    51   %   �� *
Assessor+52   %   �� 53      $��توضيحات��6%   $
 Auto��55   %    54   %   �� *Description+55   %   56      58      5:      