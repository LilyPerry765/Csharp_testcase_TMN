   M S B A M L   `   `   `  ����F  BWorldClocks, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null[2clr-namespace:BinaryComponents.WorldClocks.Windows$BinaryComponents.WorldClocks.Windows  5      9    3BinaryComponents.WorldClocks.Windows.SettingsWindow   5   	   \ XPresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      8x,http://schemas.microsoft.com/winfx/2006/xaml    5      @src2clr-namespace:BinaryComponents.WorldClocks.Windows    5        � Title$  WorldClocks Settings��5       � ShowInTaskbar .  � 
ResizeMode$ NoResize=�5       � SizeToContent$ WidthAndHeight=�5       � WindowStartupLocation$ CenterScreen=�5	       
  Dialog#��[  5
       � Icon$< 6pack://application:,,,/Resources/Icons/WorldClocks.ico��5       J� CommandBindings 5      )  #System.Windows.Input.CommandBinding  5      -   5        9   3BinaryComponents.WorldClocks.Windows.SettingsWindow	   Ok   Command# Z 5      6>     5      -   5        	   Cancel# Z	 5      6B     5      -   5        

   Add# Z
 5      6?     5      -   5           Edit# Z 5      6b     5      -   5           Remove# Z 5      6f   .��5      ]� $��Stretch=�6   ��.   	Container#��[ 6>   .+�5      ��  �� Orientation$ 
Horizontal=�6   $��Right=�6(   $��Bottom=�6D   .+�5      �� -   5        $��	_okButton��5       �� 	IsDefault . #��Z 6.     StandardButton#��[ 6Y   .��6�   OK6�   �� 5      -   5        $��_cancelButton��5       �� IsCancel . #��Z	 61   #��[ 6`   .��6�   Cancel6�   5      �� 5      $ 
Horizontal=�6   .+�5      �� -   5         ��Name$ _listBox��5      $	��400��6   $	��200��6+   ��5      �� 5      .R�5      �� $ Vertical=�6   $��5,5,5,10q�6*   .+�5   	   �� $��Stretch=�6/   $��CharacterEllipsis=�6M   $
��Bold"�6n   $��0,0,0,5q�6�   ��6   �� *
OffsetName+6�   �� 5    	   $��Stretch=�60   $��CharacterEllipsis=�6N   ��6   �� *DisplayName+6N   5!   	   5"      ��5$      �� 5%       �� 
TargetType" ��6   .�5&      ��  �� Property � �� �� Value$ Stretch=�65   5'      5)      �� 5*      $ Vertical=�6   .+�5+      �� -	   5        $��
_addButton��5+      #��Z
 6   #��[ 6K   .��6s   	_Add...6|   �� 5,      -
   5        $��_editButton��5,      #��Z 6    #��[ 6M   .��6u   
_Edit...6   �� 5-      -   5        $��_removeButton��5-      #��Z 6"   #��[ 6Q   .��6y   	_Remove6�   5.      5/      50      51      