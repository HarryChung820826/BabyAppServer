<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta name="viewport" content="width=device-width" />
    <title>Add</title>
    <link id="link" rel="stylesheet" type="text/css" href="/Content/css/one.css" />
    <link rel="stylesheet" type="text/css" href="../../Content/css/jquery-ui.css" />
	
    <script type="text/javascript" src="../../Content/js/jquery-ui-1.9.1.js"></script>
    <script type="text/javascript" src="../../Content/js/jquery-ui.js"></script>

    <script type="text/javascript" src="../../Content/js/Date.js"></script>
    <script type="text/javascript">
        $(function () {
            $(".date").removeClass("hasDatepicker");
            Set_Data();
        });
    </script>
</head>
<body>
    <div>
        <form method="post" enctype="multipart/form-data">
            <table>
                <tr>
                    <td>今日日期 : </td>
                    <td><input type="text" class="date" name="Today" /></td>
                </tr>
                <tr>
                    <td>
                        <label for="file">照片：</label>
                    </td>
                    <td>
                        <input type="file" name="files"/>
                    </td>
                </tr>
                <tr>
                    <td>內容 :</td>
                    <td>
                        <input type="text" name="context"/>
                    </td>
                </tr>
            </table>
            <input type="submit" name="Submit" value="儲存"/>
        </form>
    </div>
</body>
</html>
