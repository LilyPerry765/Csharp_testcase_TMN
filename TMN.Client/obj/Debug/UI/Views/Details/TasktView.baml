   M S B A M L   `   `   `  ����U  QEnterprise.Wpf, Version=1.1.0.0, Culture=neutral, PublicKeyToken=80f1ae88e0a8b4d1G4clr-namespace:Enterprise.Wpf;assembly=Enterprise.WpfEnterprise.Wpf  5      B >TMN, Version=2.8.0.37957, Culture=neutral, PublicKeyToken=null3clr-namespace:TMN.UserControlsTMN.UserControls 5      M ITMN.Converters, Version=1.1.0.26823, Culture=neutral, PublicKeyToken=nullG4clr-namespace:TMN.Converters;assembly=TMN.ConvertersTMN.Converters 5          TMN.Views.Details.TaskView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Br4clr-namespace:Enterprise.Wpf;assembly=Enterprise.Wpf     5      -ucclr-namespace:TMN.UserControls     5      Econv4clr-namespace:TMN.Converters;assembly=TMN.Converters     5      $	��300��5      $	��456��5      .��5	      ��   �� Orientation$  
Horizontal=�6   .+�5
   
   �� .+�5      �� $��Right=�6    �� Target 5      ��  �� ElementName$ cmbType��.��6=   عنوان:6E   �� 5      $��Right=�6   .��67   	مسير:6>   �� 5      $��Right=�6   .��67   تعداد کانال:6E   �� 5      $��Right=�6    5      �� $ 	chkIsDone��.��6?   انجام شد:6J   �� 5      $��Right=�6    5      �� $ txtFinishDate��.��6C   زمان انجام:6P   �� 5      $��Right=�6    5      �� $ 
txtComment��.��6@   توضيحات:6J   5      �� 5   
   .+�5      3clr-namespace:TMN.UserControlsTMN.UserControls 6    % TMN.UserControls.EntityComboBox  6   -ucclr-namespace:TMN.UserControls     6    -   5          Name$ cmbType��5       $	��140��5        �� DisplayMemberPath$
 Name��5        �� SelectedValuePath$ ID��5       $��0,2,0,2q�5       $
��Left=�5        �� SelectedItem 5       �� *
TaskType+5         5      -   5        $ RouteComboBox��5        �� 
IsEditable . $	��140��5!      $
 TGNO��5#      $ ID��5$      $��0,2,0,2q�5%      $
��Left=�5&       5"      �� *Route+5&      "   Enterprise.Wpf.NumericUpDown  5'      -   5          Name$ ChannelCountNumericUpDown��5'      $��0,2,0,2q�5)      $
��Left=�5*      	   Value	 5(      �� 
 �� Path$
 ChannelCount� �� Mode$ TwoWay=�5*      �� 5+      -   5         ��Name$ 	chkIsDone��5+      $	��140��5,      $��4,7,0,7q�5.      $
��Left=�5/       d� 	IsChecked 5-      �� *IsDone+51      #   Enterprise.Wpf.PersianDateBox  52      -   5          Name$ txtFinishDate��52         DisplayMode$ DateTime=�53      $��0,2,0,2q�56      $
��Left=�57         ShowCheckBox .     Date 55      �� *
FinishDate+58      �� 59      -   5         ��Name$ 
txtComment��59      " �� VerticalScrollBarVisibility$
 Auto=�5:       �� TextWrapping$
 Wrap=�5;      $	��391��5=      $��0,2,0,2q�5>      $
��Left=�5?      $	��160��5@       �� AcceptsReturn . ��5<      �� *	Comment+5A      5B      5C      5D      