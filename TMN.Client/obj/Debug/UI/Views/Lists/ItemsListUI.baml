   M S B A M L   `   `   `  ����I  ETMN.Common, Version=2.0.1.40989, Culture=neutral, PublicKeyToken=null;,clr-namespace:TMN.Assets;assembly=TMN.Common
TMN.Assets  5       TMN!   TMN.Views.Lists.ItemsListUI   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      :a,clr-namespace:TMN.Assets;assembly=TMN.Common     5      $��RightToLeft=�5      .��5      � -   5          �Name$
  Root��5       � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5	      .+�5   
   9� -   5         9�Name$ SearchExpander��5      $��0��5      ��5      �� 5      $��5q�6    J� Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5         ShadowDepth$ 1Q�6+   
جستجو5      .��5      ��  �� CornerRadius$ 5��6   $��1q�5      
������� �� Padding$ 3q�5      ���	#44FFFFFF$��3q�5      ._�5      �  � ColumnDefinitions 5      �� 5      �� 5      $
��Auto �6+   .+�5       �� -   5         ��Name$ SearchPanel��5!      $��0��5       $��Center=�5"      $
��Left=�5#      $��50��5$      �� 5%      -   5        $��SearchButton��5%      	 J� Opacity$	 .9Q�5'      $��1��5(      
 �� 	IsDefault
 . $��0q�5*       � ToolTip$ 
جستجو��5+      $	��100��5,      .��5-      �  5.      �� 5/   "   �� 50   "   .+�52      �� $��0��6$   $��RightToLeft=�53   $   $��64��54   $   ��55   $   %   TMN.Assets.ImageSourceExtension     Path$ search2.png��5;      5<      5=      5>      5?      �� 5@   
   -   5        $ ListViewHolder��5@      $��1��5A      5B      5C      