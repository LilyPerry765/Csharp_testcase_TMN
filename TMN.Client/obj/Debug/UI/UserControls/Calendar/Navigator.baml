   M S B A M L   `   `   `  ����I  ETMN.Common, Version=2.0.1.40989, Culture=neutral, PublicKeyToken=null;,clr-namespace:TMN.Assets;assembly=TMN.Common
TMN.Assets  5       TMN)   #TMN.UserControls.Calendar.Navigator   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      :a,clr-namespace:TMN.Assets;assembly=TMN.Common     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      .��5      � -   5        $��NavigationGrid��5      $��LeftToRight=�5        � ColumnDefinitions  5   
   �� 5      $	��.1* �6   �� 5	      $��* �6   �� 5
      $	��.1* �6   ��5   
   �� 5       �� 
StartPoint$	 0,0&�6"    �� EndPoint$	 0,1&�5   "   .��5      �  � Color$ White��6    � Offset$
 -.75Q�5      � 5      $ Transparent��6   $ .5Q�5      � 5      $ White��6   $
 1.75Q�5      5       � 	Resources 5   
      NavigationImage �� 
TargetType �� Property �� Value��%�   &	        �� 5      " ��5      .�5      ��  � ��$	 0,3q�5      �� 5       �@J�Opacity$ .5Q�5      5      5      .+�5    
   �� -   5        $��
LeftButton��5       	 � ToolTip$	 بعدی��5!      |�.  
������ $��0q�5$      
������ $��0��5&      $��2q�5'      
 �� 	ClickMode$
 Press=�5(      .��5*      �� #��[  5+      ��5*      %   TMN.Assets.ImageSourceExtension     Path$ navigate_left.png��5+      5,      �� 5-   
   -   5        $��RightButton��5-      $	 قبلی��5.      $��2q�5/      
������ $��0q�51      $��2��52      
������ |�.  $
 Press=�55      .��57      �� #��[  58      ��57        $ navigate_right.png��58      59      �� 5:   
   -   5        $��DisplayLabel��5<      $��1��5:      $��RightToLeft=�5;      $��Center=�5=      $��Center=�5>      $��14(�5@      ��5A      �� 5B       �� Triggers 5C      P� 5D       P� Property �@��IsMouseOver P� Value . . �5F      ��  � ��
 ��  �5G   %   5H      5J      5L      5M      5N      