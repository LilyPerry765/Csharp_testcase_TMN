   M S B A M L   `   `   `  ���� U  QEnterprise.Wpf, Version=1.1.0.0, Culture=neutral, PublicKeyToken=80f1ae88e0a8b4d1G4clr-namespace:Enterprise.Wpf;assembly=Enterprise.WpfEnterprise.Wpf  O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem I ETMN.Common, Version=2.0.1.19574, Culture=neutral, PublicKeyToken=null-%clr-namespace:TMN;assembly=TMN.CommonTMN M ITMN.Converters, Version=1.1.0.19574, Culture=neutral, PublicKeyToken=nullG4clr-namespace:TMN.Converters;assembly=TMN.ConvertersTMN.Converters B >TMN, Version=2.2.0.19582, Culture=neutral, PublicKeyToken=null'   !TMN.Views.Details.InstructionView   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R	 NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml 	    Br4clr-namespace:Enterprise.Wpf;assembly=Enterprise.Wpf 	    6sys&clr-namespace:System;assembly=mscorlib 	    7enums%clr-namespace:TMN;assembly=TMN.Common 	    Econv4clr-namespace:TMN.Converters;assembly=TMN.Converters 	    $	��336��$	��238��.����   �� Orientation$  
Horizontal=�.+��� .+��� $��Right=� �� Target ��  �� ElementName$ txtIneffect��.��	Ineffect:�� $��Right=� �� $ 	cmbIssuer��.��صادر كننده:�� $��Right=� �� $ 	cmbSource��.��	مبدا:�� $��Right=� �� $ cmbDestination��.��	مقصد:�� $��Right=� �� $ cmbExecuter��.��اجراكننده:�� $��Right=� �� $ cmbInstructionType��.��	نوع:�� $��Right=� �� $ cmbTrafficType��.��نوع ترافيك:�� $��Right=� �� $ txtIssueDate��.��تاريخ صدور:�� $��Right=� �� $ txtExecutionDate��.��تاريخ اجرا:�� $��Right=� �� $ txtAssignmentDate��.��تاريخ واگذاري:�� $��Right=� �� $ 	txtNumber��.��شماره:�� $��Right=� �� $ 	chkIsDone��.��انجام شد:�� $��Right=� �� $ txtDescription��.��توضيحات:�� .+��� -    ��Name$ txtIneffect��$	��140��$��0,2,0,2q�$
��Left=����� *
Ineffect+�� -    ��Name$ 	cmbIssuer�� �� 
IsEditable . $	��140��$��0,2,0,2q�$
��Left=� �� Text �� *Issuer+.@��� .��زيرساخت�� .��
استان3clr-namespace:TMN.UserControlsTMN.UserControls % TMN.UserControls.EntityComboBox  -ucclr-namespace:TMN.UserControls 	    -     Name$ 	cmbSource��$	��140�� �� DisplayMemberPath$
 Name��	 �� SelectedValuePath$	 ID��$��0,2,0,2q�$
��Left=�
 �� SelectedItem
 �� *Center+3clr-namespace:TMN.UserControlsTMN.UserControls   -ucclr-namespace:TMN.UserControls 	    -   $ cmbDestination��$	��140��$
 Name��$	 ID��$��0,2,0,2q�$
��Left=�
 �� *	Center1+3clr-namespace:TMN.UserControlsTMN.UserControls   -ucclr-namespace:TMN.UserControls 	    -   $ cmbExecuter��$	��140��$ FullName��$	 ID��$��0,2,0,2q�$
��Left=�
 �� *User+G4clr-namespace:TMN.Converters;assembly=TMN.ConvertersTMN.Converters �� Econv4clr-namespace:TMN.Converters;assembly=TMN.Converters 	    -   $ cmbInstructionType��$	��140��$��0,2,0,2q�$
��Left=� �� SelectedValue �� *InstructionType+.  (TMN.Converters.InstructionTypesConverter   Instance �� 	Converter# Z  � 	Resources 1&clr-namespace:System;assembly=mscorlibSystem    InstructionTypes S� 
MethodName S� 
ObjectType  System.Enum S� MethodParameters-%clr-namespace:TMN;assembly=TMN.CommonTMN  M� TypeName��%�   &	        S� 6sys&clr-namespace:System;assembly=mscorlib 	    $ 	GetValues��"   M� /enums%clr-namespace:TMN;assembly=TMN.Common  $ enums:InstructionTypes������  �� Source# [  G4clr-namespace:TMN.Converters;assembly=TMN.ConvertersTMN.Converters �� =conv4clr-namespace:TMN.Converters;assembly=TMN.Converters   �� 
IsReadOnly . $	��140��$��0,2,0,2q�|�.  $
��Left=����� *Center1.CenterType+ �� Mode$ OneWay=�)  #TMN.Converters.CenterTypesConverter   Instance# Z #   Enterprise.Wpf.PersianDateBox  -     Name$ txtIssueDate��$	��140��$��0,1,0,1q�$
��Left=�   Date �� *	IssueDate+  -   $ txtExecutionDate��$	��140��$��0,1,0,1q�$
��Left=� �� *ExecutionDate+  -	   $ txtAssignmentDate��$	��140��$��0,1,0,1q�$
��Left=� �� *AssignmentDate+�� -
   $ 	txtNumber��$	��140��$��0,2,0,2q�$
��Left=����� *Number+�� -    ��Name$ 	chkIsDone��$	��140��$��0,6,0,6q�$
��Left=� d� 	IsChecked �� *IsDone+�� -   $ txtDescription��" �� VerticalScrollBarVisibility$
 Auto=� �� AcceptsReturn . $	��140��$��0,2,0,2q�$
��Left=�$��42������ *Description+