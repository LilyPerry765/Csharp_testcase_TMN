   M S B A M L   `   `   `  ����U  QEnterprise.Wpf, Version=1.1.0.0, Culture=neutral, PublicKeyToken=80f1ae88e0a8b4d1G4clr-namespace:Enterprise.Wpf;assembly=Enterprise.WpfEnterprise.Wpf  5      O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5      I ETMN.Common, Version=2.0.1.40989, Culture=neutral, PublicKeyToken=null-%clr-namespace:TMN;assembly=TMN.CommonTMN 5      M ITMN.Converters, Version=1.1.0.26823, Culture=neutral, PublicKeyToken=nullG4clr-namespace:TMN.Converters;assembly=TMN.ConvertersTMN.Converters 5      ;,clr-namespace:TMN.Assets;assembly=TMN.Common
TMN.Assets 5      B >TMN, Version=2.8.0.37957, Culture=neutral, PublicKeyToken=null"   TMN.Views.Details.CenterView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R	 NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml 	    5      Br4clr-namespace:Enterprise.Wpf;assembly=Enterprise.Wpf 	    5      6sys&clr-namespace:System;assembly=mscorlib 	    5      7enums%clr-namespace:TMN;assembly=TMN.Common 	    5      Econv4clr-namespace:TMN.Converters;assembly=TMN.Converters 	    5      :a,clr-namespace:TMN.Assets;assembly=TMN.Common 	    5      $��RightToLeft=�5	      $��Tahoma)�5
      $	��370��5      $	��501��5      .��5      �   � 	Resources  5   
    �� 
TargetType �� Property �� Value��%�   '��       �� 5      " ��6   .�5      ��  � ��$ Right=�5      �� 5       � ��$ Center=�5      5      5       � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5       $
��Auto �6   �� 5!      $
��Auto �6   �� 5"      $
��107* �6    � ColumnDefinitions 5$   
   �� 5%      $
��Auto �6   �� 5&      $
��Auto �6    �� 5'      $
��Auto �6    �� 5(      $
��Auto �6    �� 5)      $
��Auto �6   .+�5+   
   �� $��0��6   $��1��5,      .��6   سوييچ:6%   �� 5-   
   $��2��6   $��1��5.      .��6   نوع مركز:6(   �� 5/   
   $��0��6   $��2��50      .��6   منطقه:6%   �� 51   
   $��0��6   .��6    نام مركز:6+   �� 52   
   $��2��6   $��2��53      .��6   كد:6"   �� 54   
   $��0��6   $��5��55      .��6   
PointCode:6)   �� 56   
   $��2��6   $��5��57      .��6   FX:6"   �� 58   
   $��0��6   $��6��59      .��6   شماره تست:6)   �� 5:   
   $��2��6   $��6��5;      .��6   شماره تماس:6*   �� 5<   
   $��0��6   $��7��5=      .��6   عرض جغرافيايي:6-   �� 5>   
   $��2��6   $��7��5?      .��6   طول جغرافيايي:6-   �� 5@   
   $��9��6   $��0,-1,0,1q�5A      .��6"   توضيحات:6,   3clr-namespace:TMN.UserControlsTMN.UserControls 5B      % TMN.UserControls.EntityComboBox  6
   -ucclr-namespace:TMN.UserControls 	    6   -   5          Name$ 	cmbSwitch��5C      $	��150��5D       �� DisplayMemberPath$
 Name��5F       �� SelectedValuePath$ ID��5G      $��0,2,0,2q�5H      $��1��5I      $��1��5J      	 �� SelectedItem	 5E      �� *
SwitchType+5J      �� 5K   
   -   5        
 ��Name$
 
cmbRegions��5K      $	��150��5L      $��0,2,0,2q�5M      $��2��5N      $��1��5O      $
 Name��5P      	 5Q      �� *Region+�� 5R   
   -   5        $
 cmbCenterType��5R      $	��150��5S      $��0,2,0,2q�5T      $��1��5U      $��3��5V      $ Value��5W      ��5X        TMN.Assets.EnumDataSource     EnumType$ enums:CenterTypesL� �� SelectedValue 5Y      �� *
CenterType+�� 5Z   
   -   5         ��Name$ txtName��5Z      $	��150��5[      $��0,2,0,2q�5]      $��1��5^      ��5\      �� *Name+5^      �� 5_   
   -   5        $ txtCode��5_      $	��150��5`      $��0,2,0,2q�5b      $��2��5c      $��3��5d      ��5a      �� *Code+5d      �� 5e   
   $��2��6   $��3��5f      .��6   
User Name:6)   �� 5g   
   -   5        $ userNameTextBox��5g      $	��150��5h      $��0,2,0,2q�5j      $��3��5k      $��3��5l      ��5i      �� *
UserName+5l      �� 5m   
   $��3��6   .��6   IP:6"   �� 5n   
   $��4��6   .��6   کلمه عبور:6)   G� 5o   
   -   5         G�Name$ password��5q      $��1��5o      $��4��5p      5q      �� 5r   
   $��4��6   $��2��6   .��6-   تکرار کلمه عبور:6?   G� 5s   
   -   5        $ passwordRepeat��5u      $��3��5s      $��4��5t      5u      �� 5v   
   -	   5        $ 	ipTextBox��5v      $	��150��5w      $��0,2,0,2q�5y      $��1��5z      $��3��5{      ��5x      �� *	IPAddress+5{      �� 5|   
   -
   5        $ txtPointCode��5|      $	��150��5}      $��0,2,0,2q�5      $��1��5�      $��5��5�      ��5~      �� *	PointCode+5�      �� 5�   
   -   5        $ txtFX��5�      $	��150��5�      $��0,2,0,2q�5�       �� 	MaxLength$ 10��5�      $��3��5�      $��5��5�      ��5�      �� *FX+5�      �� 5�   
   -   5        $ txtTest��5�      $	��150��5�      $��0,2,0,2q�5�      $ 15��5�      $��1��5�      $��6��5�      ��5�      �� *TestNo+5�      �� 5�   
   -   5        $ 
txtContact��5�      $	��150��5�      $��0,2,0,2q�5�      $ 30��5�      $��3��5�      $��6��5�      ��5�      �� *	ContactNo+5�      �� 5�   
   -   5        $ txtLatitude��5�      $	��150��5�      $��0,2,0,2q�5�      $��1��5�      $��7��5�      ��5�      �� *
Latitude+5�      �� 5�   
   -   5        $ txtLongitude��5�      $	��150��5�      $��0,2,0,2q�5�      $��3��5�      $��7��5�      ��5�      �� *	Longitude+5�      �� 5�   
   $��0��6   $��8��5�      .��6   Dest:6$   &   System.Windows.Controls.DataGrid  5�   
   -   5          Name$ destDataGrid��5�      $��8��5�      $��0,5,0,5q�5�         SelectionMode$ Single=�5�         RowHeaderWidth$ 0Q�5�         SelectionUnit$ FullRow=�5�         HeadersVisibility$ Column=�5�      $��1��5�      $��3��5�      $��80��5�      ��5�      �� *Dests+   Columns 5�      0  *System.Windows.Controls.DataGridTextColumn  5�      ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$	 300 6%      Header$ عنوان Dest��5�   %   1  +System.Windows.Controls.DataGridBoundColumn   Binding 5�   %   �� *Name+5�      �� 5�   
   -   5        $ txtDescription��5�      " �� VerticalScrollBarVisibility$
 Auto=�5�       �� AcceptsReturn . $��3��5�      $��0,2,0,2q�5�      $��50��5�      $��1��5�      $��9��5�      ��5�      �� *Description+5�      �� 5�   
   -   5         ��Name$ DefaultCenterCheckBox��5�       d� 	IsChecked$
 TrueY�5�      $��1��5�      $��11��5�      $	��Top=�5�      .��6+   مرکز جاری66   5�      5�      