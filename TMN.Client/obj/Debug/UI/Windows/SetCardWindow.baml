   M S B A M L   `   `   `  ����I  ETMN.Common, Version=2.0.1.40989, Culture=neutral, PublicKeyToken=null;,clr-namespace:TMN.Assets;assembly=TMN.Common
TMN.Assets  5   	   U QEnterprise.Wpf, Version=1.1.0.0, Culture=neutral, PublicKeyToken=80f1ae88e0a8b4d1G4clr-namespace:Enterprise.Wpf;assembly=Enterprise.WpfEnterprise.Wpf 5   	   B >TMN, Version=2.8.0.37957, Culture=neutral, PublicKeyToken=null3clr-namespace:TMN.UserControlsTMN.UserControls 5   	   "   TMN.UI.Windows.SetCardWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   :a,clr-namespace:TMN.Assets;assembly=TMN.Common     5   	   Dent4clr-namespace:Enterprise.Wpf;assembly=Enterprise.Wpf     5   	   -ucclr-namespace:TMN.UserControls     5   	     � Title$  تخصيص کارت��5   	    � 
ResizeMode$ NoResize=�5   	    � WindowStartupLocation$ CenterOwner=�5	   	   $��Tahoma)�5
   	   $	��182��5   	   $	��279��5   	      lightBlueShadow#��[  5   	   $��RightToLeft=�5   	   .��5      ]� $��RightToLeft=�6   ��. .+�5   
   �� -   5         ��Name$ ControlPanel��5      $��Bottom=�5      $��
-1,0,-1,-1q�5      $��32��5      
����ح�$��1q�5      ._�5      � .+�5      "  TMN.UserControls.ImageButton  -   5          Name$ btnOK��5   "   "  TMN.UserControls.ImageButton   Click$ btnOK_Click��5    "   $��12,0,0,0q�5   "   $
��Left=�5   "   $��75��5   "   $��23��5   "   $��Center=�5   "    �� 	IsDefault .    Text$ 
تاييد��5!   "      ImageSource 5   "   %   TMN.Assets.ImageSourceExtension  	   Path$	 	check.png��5!   "     5"      -   5        $ 	btnCancel��5$   !   $ انصراف��5"   !   $��93,0,0,0q�5#   !   $��23��5%   !   $��Center=�5&   !   $
��Left=�5'   !   $��75��5(   !   
 �� IsCancel
 .  5)   !     $	 
cancel.png��5*   !   5+      5,      � 5-   
   $	��0,5q�6    � ColumnDefinitions 5.      �� 5/      $��* �6#   �� 50      $��3* �6#    � RowDefinitions 52      �� 53      �� 54      �� 55      �� 56      $
��Auto �6!    � 	Resources 58       �� 
TargetType �� Property �� Value��%c  '��       '�� {     �� 59      " ��6   .�5:      ��  � ��$ Center=�5;      �� 5<       � ��$ Right=�5=      5>      �� 5?      " ��6   .�5@      ��  � ��$ Center=�5A      �� 5B       � ��$
 Left=�5C      �� 5D       � ��$	 100��5E      �� 5F       �@��
IsEditable . 5G      5H      5I      .+�5J      �� $��0��6   $��0��5K      .��6!   	راک:6'   �� 5L      $��0��6   $��1��5M      .��6!   	شلف:6'   �� 5N      $��0��6   $��2��5O      .��6!   	کارت:6(   �� 5P      -   5         ��Name$ RackComboBox��5P      $��1��5Q      $��0��5R       �� DisplayMemberPath$
 Name��5T       �� SelectedValuePath$ ID��5U      �� 5V      -   5        $ ShelfComboBox��5V      $��1��5W      $��1��5X      $
 Name��5Y      $ ID��5Z      5[      �� 5\      -   5        $ CardComboBox��5\      $��1��5]      $��2��5^      $
 Name��5_      $ ID��5`      5a      5b      5c      