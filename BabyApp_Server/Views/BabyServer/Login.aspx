<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta name="viewport" content="width=device-width" />
    <title>Login</title>
</head>
<body>
    <form method="post">
        <table>
            <tr>
                <td>帳號 : </td>
                <td><input type="text" name="UserName" /></td>
            </tr>    
            <tr>
                <td>密碼 : </td>
                <td><input type="password" name="UserPwd" /></td>
            </tr>    
        </table>
        <input type="submit" value="登入" name="Login"/>
    </form>
</body>
</html>
