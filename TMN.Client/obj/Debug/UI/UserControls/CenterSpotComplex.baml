   M S B A M L   `   `   `  ����B  >TMN, Version=2.8.0.37957, Culture=neutral, PublicKeyToken=null3clr-namespace:TMN.UserControlsTMN.UserControls  5      I ETMN.Common, Version=2.0.1.40989, Culture=neutral, PublicKeyToken=null;,clr-namespace:TMN.Assets;assembly=TMN.Common
TMN.Assets 5      (    "TMN.UserControls.CenterSpotComplex   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -ucclr-namespace:TMN.UserControls     5      :a,clr-namespace:TMN.Assets;assembly=TMN.Common     5        � ToolTip  5      �� *Center.IPAddress+ � 	Resources 5
      3  -System.Windows.Media.Effects.DropShadowEffect   shadowEffect   ShadowDepth   Opacity��%:   &	          5   
   $ 0Q�5      $ 1Q�5      5      .��5      �� $��90��6   $��Center=�6   .+�5   
   �� $	��Top=�6   .+�5      �� -   5        $��
muteBorder��5      
������ $��3q�6A   ._�5   
   ]� .+�5      �� -   5        $��70��5      $��50��5       �� CornerRadius$ 5��5      $��1q�5       � Cursor$
 Hand��5      
���   �
���   �._�5      �� 
�������$��1q�5      $ 4��5      ._�5       � -   5         �Name$ containerGrid��5        � RowDefinitions 5!      �� 5"      �� 5#       5%       �� 
TargetType	 �� Property
 �� Value��%c  '��       '�� �     �� 5&      " ��6$   .�5'   "   �� 	 � ��

 �����5(   %   �� 5)   "   	 � ��$
 Center=�5*   %   �� 5+   "   	 � ��$
 Center=�5,   %   �� 5-   "   	 � ��$
 0q�5.   %   5/      �� 50      " ��6$   .�51   "   �� 	 � ��$

 0.01q�52   %   �� 53   "   	 �@��CornerRadius$
 3��54   %   55      56      .+�57      �� -   5         ��Name$ switchBorder��58      $ 3,3,0,0��57   !   ��59      
� 5:   "    
� RadiusX$ 1Q�66    
� RadiusY$ .6Q�5;   2    
� Center$ .5,.5&�5<   2    
� GradientOrigin$
 .5,0&�5=   2   .��5>   &   � -   5        SwitchColor�� � Offset$ 0Q�5?   /   5@   #   5B      �� 5C      -   5        $ sensorBorder��5D      $��1��5C   !   $ 0,0,3,3��5E      ��5F      
� 5G   "   $ 1Q�66   $ .6Q�5H   2   $ .5,.5&�5I   2   $
 .5,1&�5J   2   .��5K   &   � -   5        SensorColor��$ 0Q�5L   /   5M   #   5O      �� 5P      -   5         ��Name$ switchNameLabel��5R      $��0��5P       $��16(�5Q      ��5P   -   �� *Center.SwitchType.Name+5R      �� 5S      -	   5        $ sensorLabel��5T      $��1��5S       $��Normal"�5U       J� Opacity$ .8Q�5V      $��12(�5W      $��Italic$�5X      .��6/   Sensor67   �� 5Y      $��2��6!   $ .6Q�5Z        GlassFilter#��[ 5[      5]      5^      5`      5d      5e      !   TMN.UserControls.MuteCenter  5g      -
   5          Name$ 
muteButton��5g      {�.  $y�Hidden=�6C      IsMute . 
 �� Top$ 20��6g    �� Left$ 50��6w   5h      �� 5i   
    �� Bottom$ 0��6   $��	B Nazanin)�5j      $
��Bold"�5k      $��25(�5l       �� TextAlignment$ Center=�5n      $��Bottom=�5o      ��5m      �� *Center.Name+��5p      �� 5q       �� EndPoint$	 0,1&�6&   .��5r      � $ 0Q�6#    � Color$ Black��5s   #   � 5t      $ .5Q�6"   $ Black��5u   "   � 5v      $	 1.5Q�6%   $
 gray��5w   %   5x       J� Effect 5z        5{      $ 1Q�6#      	Direction$	 -90Q�5|   #   $ 1Q�5}   #      
BlurRadius$ 1Q�5~   #      Color$ White��5   #   5�   "   5�       � ContextMenu 5�      �� 5�   
   .@�5�      m� -   5          m�Name$  connectMenuItem��5�      $��اتصال به مرکز��5�      5�      m� 5�      -   5        $  muteMenuItem��5�      $��قطع صدا��5�      ! m� IsCheckable! . " m� 	IsChecked" .  5�   E   m� 5�      -   5        $  muteCriticalMenuItem��5�      $��قطع صدای Critical��5�      ! . " .  5�   M   m� 5�      -   5        $  muteMajorMenuItem��5�      $��قطع صدای Major��5�      ! . " .  5�   J   m� 5�      -   5        $  muteMinorMenuItem��5�      $��قطع صدای Minor��5�      ! . " .  5�   J   5�      5�      