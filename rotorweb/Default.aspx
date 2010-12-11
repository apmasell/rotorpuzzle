<%@ Page Language="C#" Inherits="name.masella.rotorpuzzle.Default" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html>
<head runat="server">
	<title>Centrifuge Rotor Configuration</title>
</head>
<body>
	<h1>Centrifuge Rotor Configuration</h1>
	<form id="cfgform" runat="server">
		Samples: 
		<asp:TextBox id="samples" runat="server"/>
		<asp:CompareValidator id="samplecheck" runat="server" ErrorMessage="You must enter a number" ControlToValidate="samples" Type="Integer" Operator="DataTypeCheck"></asp:CompareValidator>
		Rotor Slots:
		<asp:TextBox id="slots" runat="server"/>
		<asp:CompareValidator id="slotscheck" runat="server" ErrorMessage="You must enter a number" ControlToValidate="slots" Type="Integer" Operator="DataTypeCheck"></asp:CompareValidator>
		<asp:Button id="calc" runat="server" Text="→" OnClick="configure" CausesValidation="true"/>
		<asp:Label id="status" runat="server"/>
	</form>
	<img id="rotor" runat="server" alt="Rotor Configuration"/>
</body>
</html>
