   M S B A M L   `   `   `  ����   TMN!    TMN.UserControls.DDFDisplay   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -   $��Stretch=�$��Stretch=�$��0q�$��LeftToRight=�.���� -   $��bgBorder��$��2q�  �� CornerRadius$  5��._��� $  5�����	#FFFFFFFF$��1q�   GlassFilter#��[  ._�� ����  �� 
StartPoint$	 0,0&� �� EndPoint$	 0,1&�.���  � Offset$ 0Q� � Color$ 	#70FFFFFF��� $ .5Q�$ 	#30FFFFFF��� $ .6Q�$ 	#30FFFFFF��� $ 1Q�$ 	#70FFFFFF�� � ColumnDefinitions �� $
��Auto ��� �� $
��Auto � � RowDefinitions �� $
��Auto ��� �� $
��Auto �.+��� $	��3,0q�$��Center=�$��0��$
��Bold"�$��1������ *SingleLink.Address+�� -    ��Name$  DestinationItemsStackPanel��$��2��$��1��.+��� $	��3,0q�$
��Bold"�$��Center=����� *" SingleLink.UniqueRoute.RouteName+�� $	��3,0q�$
��Bold"�$��Center=����� *SingleLink.UniqueRoute.TGNO+�� $	��3,0q�$��Center=�$��Center=�$��1��$��2������ *Number+��  �� ContentStringFormat$ sys-{0}��$	��3,0q�$��Center=�$��Center=�$��1��$��0������ *SingleLink.Sys+�� -   	 ��Name$	 txtBox��$��1��$��1�����	#AAFFFFFF$y�	Collapsed=�
 �� TextWrapping$

 Wrap=� �� 	MaxLength$ 50��$��Center=�$��Center=����� *
Foreground+ �� ElementName$ 
lblDisplay������ *Description+�� -    ��Name$ 
lblDisplay��$��1��$��1��$��Center=�$��Center=����� *Description+ �� Mode$ TwoWay=�