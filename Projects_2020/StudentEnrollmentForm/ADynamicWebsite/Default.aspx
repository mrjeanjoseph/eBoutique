<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>LOGIN</title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="login-wrap">
            <div class="login-html">
                <input id="tab-1" type="radio" name="tab" class="sign-in" checked><label for="tab-1" class="tab">Sign In</label>
                <input id="tab-2" type="radio" name="tab" class="sign-up"><label for="tab-2" class="tab">Sign Up</label>
                <div class="login-form">
                    <div class="sign-in-htm">
                        <div class="group">
                            <label for="user" class="label">Username</label>
                            <asp:TextBox ID="tb_username" runat="server" CssClass="input"></asp:TextBox>
                        </div>
                        <div class="group">
                            <label for="pass" class="label">Password</label>
                            <asp:TextBox ID="tb_pass" runat="server" TextMode="Password" CssClass="input"></asp:TextBox>
                        </div>
                        <div class="group">
                            <asp:Button ID="btn_login" runat="server" Text="Login" CssClass="input"
                                OnClick="btn_login_Click" Style="cursor: pointer" />
                        </div>
                        <div class="hr"></div>
                        <div class="foot-lnk">
                            <asp:Label ID="lb_msg" runat="server" Text="" ForeColor="Red"></asp:Label><br />
                        </div>
                    </div>

                    <div class="sign-up-htm">
                        <div class="group">
                            <label for="user" class="label">Full Name</label>
                            <asp:TextBox ID="tb_fullname" runat="server" CssClass="input"></asp:TextBox>
                        </div>
                        <div class="group">
                            <label for="pass" class="label">Username</label>
                            <asp:TextBox ID="tb_usernameS" runat="server" CssClass="input"></asp:TextBox>
                        </div>
                        <div class="group">
                            <label for="pass" class="label">Password</label>
                            <asp:TextBox ID="passS" runat="server" CssClass="input" TextMode="Password"></asp:TextBox>
                        </div>
                        <div class="group">
                            <asp:Button ID="btn_signup" runat="server" Text="Sign Up"
                                OnClick="btn_signup_Click" CssClass="button" style="cursor:pointer" />
                        </div>
                        <hr style="height: 2px; color: rgba(255,255,255,.2);" />
                        <div class="foot-lnk">
                            <label for="tab-1">Already a Member?</label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>


<footer>
    <p>Developing Valuable Client using Technology
        <br />
        (DVC/T)</p>
    <p>
        Contact information:
        <br />
        Email Adress: <a href="mailto:jeanjosephenterprise.com">JeanJoseph Enterprise</a><br />
        Phone Number: (919) 500-0936
    </p>
</footer>

