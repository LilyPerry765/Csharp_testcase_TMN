   M S B A M L   `   `   `  ����  TMN,    &TMN.UserControls.Calendar.CalendarItem   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      -   5          >�Name$  calItem��5       � 	Resources 5         HighlightBrush1 �� 
StartPoint �� EndPoint � Color � Offset  HighlightBrush��%]  &	        &	 �     �� 5   
   $	 1,0&�5      $	 0,1&�5      .��5	      � $ #F6DB6C��6   $ 0Q�5
      � 5      $ #F8A82A��6   $ .8Q�5      � 5      $ #F6CA46��6   $ 1Q�5      5      �� 5   
   $	 1,0&�5      $	 0,1&�5      .��5      � $ #FF0000��6   $ 0Q�5      5      5      .��5      �� -   5        $��Border��5       �� CornerRadius$ 5��5      $��1q�5      $��1q�5      ��5   
   
� 5       
� RadiusX$ 1Q�6"    
� RadiusY$ 1Q�5   "   	 
� GradientOrigin$		 1,0&�5   "   .��5      � $ White��6   $	 -.5Q�5       6.   � 5!      $ Transparent��6   $ 1Q�5"      5#      ._�5%   
   m� -   5        $��.5q�5%      $��-24,0,-54,0q�5'      ��5(      �� 5)      
 �� Triggers
 5*      P� 5+       P� Property �@��IsMouseOver P� Value . . �5-      ��  �� Property � �� �� Value# [ 5.   %   5/      51      ��53      � 54       � RowDefinitions 55      �� 56      $
��Auto �6(   �� 57      $��* �6(   .+�59      �� -   5        $��headerTextBlock��59       $	��Top=�5:       $��0��5;       $��2q�5<       $
��Left=�5=       � 5>      -   5        $��contentPlaceHolder��5>      $��1��5?      .+�5@      �� -   5         ��Name$ TemplateControl��5@   "   5A      5B      5D      5E      5F      