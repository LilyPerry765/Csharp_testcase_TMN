   M S B A M L   `   `   `  ����  TMN!    TMN.UserControls.DDFDisplay   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      -   5        $��Stretch=�5      $��Stretch=�5      $��0q�5      $��LeftToRight=�5      .��5	      �� -   5        $��bgBorder��5	      $��2q�5
        �� CornerRadius$  5��5      ._�5   
   �� $  5��6   ���	#FFFFFFFF$��1q�5         GlassFilter#��[  5      ._�5      � ��5      �� 5       �� 
StartPoint$	 0,0&�6*    �� EndPoint$	 0,1&�5   *   .��5      �  � Offset$ 0Q�6'    � Color$ 	#70FFFFFF��5   '   � 5      $ .5Q�6'   $ 	#30FFFFFF��5   '   � 5      $ .6Q�6'   $ 	#30FFFFFF��5   '   � 5      $ 1Q�6'   $ 	#70FFFFFF��5   '   5       � ColumnDefinitions 5      �� 5       $
��Auto �6'   �� 5!      �� 5"      $
��Auto �6'    � RowDefinitions 5$      �� 5%      $
��Auto �6$   �� 5&      �� 5'      $
��Auto �6$   .+�5)      �� $	��3,0q�5*      $��Center=�5+      $��0��5,      $
��Bold"�5-      $��1��5.      ��5)      �� *SingleLink.Address+5.      �� 5/      -   5         ��Name$  DestinationItemsStackPanel��5/      $��2��50      $��1��51      .+�52      �� $	��3,0q�6   $
��Bold"�54      $��Center=�55      ��53      �� *" SingleLink.UniqueRoute.RouteName+55      �� 56      $	��3,0q�6   $
��Bold"�58      $��Center=�59      ��57      �� *SingleLink.UniqueRoute.TGNO+59      5:      �� 5<      $	��3,0q�5=      $��Center=�5>      $��Center=�5?      $��1��5@      $��2��5A      ��5<      �� *Number+5A      �� 5B       �� ContentStringFormat$ sys-{0}��5C      $	��3,0q�5D      $��Center=�5E      $��Center=�5F      $��1��5G      $��0��5H      ��5B      �� *SingleLink.Sys+5H      �� 5I      -   5        	 ��Name$	 txtBox��5I      $��1��5J      $��1��5K      ���	#AAFFFFFF$y�	Collapsed=�5N      
 �� TextWrapping$

 Wrap=�5O       �� 	MaxLength$ 50��5P      $��Center=�5Q      $��Center=�5U      ��5L      �� *
Foreground+ �� ElementName$ 
lblDisplay����5T      �� *Description+5U      �� 5V      -   5         ��Name$ 
lblDisplay��5V      $��1��5W      $��1��5X      $��Center=�5Y      $��Center=�5Z      ��5[      �� *Description+ �� Mode$ TwoWay=�5\      5^      5_      5`      5a      