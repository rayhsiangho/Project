<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<TwitterTweetsApp.Models.TwitterTweetModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Home Page
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: ViewData["Message"] %></h2>
   <script type="text/javascript" src="<%=Url.Content("~/Scripts/jquery-1.10.2.min.js") %>"></script>
     <script type="text/javascript">
         function GetTweetData() {
             GetTweetList();
             GetTweetTotal();
         }
         function GetTweetTotal() {
             var URL = '<%=Url.Content("~/Home/ViewTweetTotal") %>'
             //alert("Ok");
             $.post(URL, function (data) {
                 if (data != null) {
                     var objJSON = jQuery.parseJSON(data);
                     $("#Total").html("");
                     if (objJSON != null) {
                         $("#Total").append("<table  id='listtable' > <tr id='listheader'><th width='100px' align='left'>Tweet Account Name</th><th width='300px' align='left'>Total Number Of Mentioned In Tweet For Two Week</th> <th width='300px' align='left'>Total Number Of Tweet For Two Week</th></tr>");

                         for (var i = 0; i < objJSON.length; i++) {
                             $("#Total").append("<tr >");
                             $("#Total").append("<td width='100px' align='left'>" + objJSON[i].AccountName + "</td>");
                             $("#Total").append("<td width='300px' align='left'>" + objJSON[i].TotalNumberOfMentionedInTweetForTwoWeek + "</td>");
                             $("#Total").append("<td width='300px' align='left'>" + objJSON[i].TotalNumberOfTweetForTwoWeek + "</td>");
                             $("#Total").append("</tr >");
                         }
                         $("#Total").append("</table>");
                     }
                 }
             });
         }
         function GetTweetList() {
             var URL = '<%=Url.Content("~/Home/ViewTweetList") %>'
             //alert("Ok");
             $.post(URL, function (data) {
                 if (data != null) {
                     var objJSON = jQuery.parseJSON(data);
                     $("#Result").html("");
                     if (objJSON != null) {
                         $("#Result").append("<table  id='listtable' > <tr id='listheader'><th width='100px' align='left'>Tweet Account Name</th><th width='200px' align='left'>Tweet ID</th><th width='200px' align='left'>Tweet Created Date</th> <th width='300px' align='left'>Tweet Text</th></tr>");

                         for (var i = 0; i < objJSON.length; i++) {
                             $("#Result").append("<tr >");
                             $("#Result").append("<td width='100px' align='left'>" + objJSON[i].AccountName + "</td>");
                             $("#Result").append("<td width='200px' align='left'>" + objJSON[i].Id + "</td>");
                             $("#Result").append("<td width='200px' align='left'>" + objJSON[i].Created_at + "</td>");
                             $("#Result").append("<td width='300px' align='left'>" + objJSON[i].Text + "</td>");
                             $("#Result").append("</tr >");
                         }
                         $("#Result").append("</table>");
                     }
                 }
             });
         }

  </script>
    <p>
    <div id="Result"></div>
    <div id="Total"></div>
      <input type="Submit" value="Click to View Tweet"  onclick="javascript:GetTweetData();"/>
     
    </p>
</asp:Content>
