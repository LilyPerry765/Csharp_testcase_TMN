   M S B A M L   `   `   `  ����U  QEnterprise.Wpf, Version=1.1.0.0, Culture=neutral, PublicKeyToken=80f1ae88e0a8b4d1G4clr-namespace:Enterprise.Wpf;assembly=Enterprise.WpfEnterprise.Wpf  5   	   I ETMN.Common, Version=2.0.1.40989, Culture=neutral, PublicKeyToken=null;,clr-namespace:TMN.Assets;assembly=TMN.Common
TMN.Assets 5   	   B >TMN, Version=2.8.0.37957, Culture=neutral, PublicKeyToken=null3clr-namespace:TMN.UserControlsTMN.UserControls 5   	   M ITMN.Converters, Version=1.1.0.26823, Culture=neutral, PublicKeyToken=nullG4clr-namespace:TMN.Converters;assembly=TMN.ConvertersTMN.Converters 5   	   &    TMN.UI.Windows.ShiftConfigWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Be4clr-namespace:Enterprise.Wpf;assembly=Enterprise.Wpf     5   	   :a,clr-namespace:TMN.Assets;assembly=TMN.Common     5   	   ,uclr-namespace:TMN.UserControls     5   	   Econv4clr-namespace:TMN.Converters;assembly=TMN.Converters     5   	     � Title$'  !تنظيم ساعت شيفت ها��5   	    � 
ResizeMode$ NoResize=�5	   	   $��Tahoma)�5
   	   $	��211��5   	   $	��309��5   	      lightBlueShadow#��[  5   	   .��5      � $��RightToLeft=�6    � RowDefinitions 5   
   �� 5      �� 5      $
��Auto �6   $��31��5      .+�5   
   �� $��5q�6   $%��ساعت شروع شيفت ها��5      .��5      � $��0��6   $��30,10q�5       5      �� 5      �� 5      �� 5       � ColumnDefinitions 5      �� 5      $
��Auto �6'   �� 5      $
��Auto �6'   .+�5!      �� $��شيفت صبح:��6   $��0��5"      $��0��5#      $��Right=�5$      $��Center=�5%      �� 5&      $��شيفت عصر:��6   $��1��5'      $��0��5(      $��Right=�5)      $��Center=�5*      �� 5+      $��شيفت شب:��6   $��2��5,      $��0��5-      $��Right=�5.      $��Center=�5/      #   Enterprise.Wpf.PersianDateBox  50      -   5          Name$ 
morningBox��50   #      DisplayMode$
 Time=�51   #   $��0��52   #   $��1��53   #   $��23��54   #   $��Center=�55   #     56      -   5        $ afterNoonBox��56   #   $
 Time=�57   #   $��1��58   #   $��1��59   #   $��23��5:   #   $��Center=�5;   #     5<      -   5        $ nightBox��5<   #   $
 Time=�5=   #   $��2��5>   #   $��1��5?   #   $��23��5@   #   $��Center=�5A   #   5B      5C      �� 5D   
   -   5         ��Name$ ControlPanel��5D      $��1��5E      $��
-1,0,-1,-1q�5F      $��32��5G      
����ح�$��1q�5I      ._�5J      � .+�5K      "  TMN.UserControls.ImageButton  -   5          Name$ btnOK��5L       "  TMN.UserControls.ImageButton   Click$ btnOK_Click��5U       $��12,0,0,0q�5K       $
��Left=�5M       $��Stretch=�5N       $��75��5O       $��23��5P       $��Center=�5Q       	 �� 	IsDefault	 . 
   Text$
 
تاييد��5T          ImageSource 5S       %  TMN.Assets.ImageSourceExtension     Path$ 	check.png��5U         5V      -   5        $ 	btnCancel��5W   !   $��93,3.5,0,3.5q�5V   !   $��Stretch=�5X   !    �� IsCancel . $
 انصراف��5Z   !   $
��Left=�5\   !   $��78��5]   !    5[   !     $ 
cancel.png��5]   !   5^      5_      5`      5a      