   M S B A M L   `   `   `  ���� B  >TMN, Version=2.2.0.19582, Culture=neutral, PublicKeyToken=null3clr-namespace:TMN.UserControlsTMN.UserControls  I ETMN.Common, Version=2.0.1.19574, Culture=neutral, PublicKeyToken=null;,clr-namespace:TMN.Assets;assembly=TMN.Common
TMN.Assets !    TMN.UserControls.CenterSpot   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     -ucclr-namespace:TMN.UserControls     :a,clr-namespace:TMN.Assets;assembly=TMN.Common       � ToolTip  �� *Center.IPAddress+ � 	Resources 3  -System.Windows.Media.Effects.DropShadowEffect   shadowEffect   ShadowDepth   Opacity��%   &	          $ 0Q�$ 1Q�.���� $��80��$��Center=�.+��� $	��Top=�.+�]� .+��� -   $��70��$��50�� �� CornerRadius$ 5��$��1q� � Cursor$
 Hand��
���   �
���   �._��� 
�������$��1q�$ 4��._�� -    �Name$ containerGrid�� � RowDefinitions �� ��   �� 
TargetType	 �� Property
 �� Value��%�   '��       '�� n     �� " ��.��� 	 � ��

 ������� 	 � ��$
 Center=��� 	 � ��$
 Center=��� 	 � ��$
 0q��� " ��.��� 	 � ��$

 0.01q��� 	 �@��CornerRadius$
 3��.+��� -    ��Name$ switchBorder��$ 3,3,0,0����
�  
� RadiusX$ 1Q� 
� RadiusY$ .6Q� 
� Center$ .5,.5&� 
� GradientOrigin$
 .5,0&�.��� -   SwitchColor�� � Offset$ 0Q��� -   $ sensorBorder��$��1��$ 0,0,3,3����
� $ 1Q�$ .6Q�$ .5,.5&�$
 .5,1&�.��� -   SensorColor��$ 0Q��� -    ��Name$ switchNameLabel��$��0��$��16(����� *Center.SwitchType.Name+�� -   $ sensorLabel��$��1��$��Normal"� J� Opacity$ .8Q�$��12(�$��Italic$�.��Sensor�� $��2��$ .6Q�  GlassFilter#��[ "   TMN.UserControls.SoundButton  -	     Name$ 
muteButton��{�.  $y�Hidden=�   IsMute . 
 �� Top$ 20�� �� Left$ 50����  �� Bottom$ 0��$��	B Nazanin)�$
��Bold"�$��25(� �� TextAlignment$ Center=�$��Bottom=����� *Center.Name+����  �� EndPoint$	 0,1&�.��� $ 0Q� � Color$ Black��� $ .5Q�$ Black��� $	 1.5Q�$
 gray�� J� Effect   $ 1Q�   	Direction$	 -90Q�$ 1Q�   
BlurRadius$ 1Q�   Color$ White�� � ContextMenu �� .@�m� -
     m�Name$  connectMenuItem��$��اتصال به مرکز��m� -   $  ackMenuItem��$��تایید مرکز��m� -   $  muteMenuItem��$��قطع صدا��! m� IsCheckable! . " m� 	IsChecked" .  