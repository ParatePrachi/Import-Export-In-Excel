
$(document).ready(function () {
    getmainstudent();
});


var Savestudent = function () {
    var studentname = $("#txtstudentname").val();
    var studentaddress = $("#txtaddress").val();
    var city = $("#ddlcity").val();
    var model = { StudentName: studentname, StudentAddress: studentaddress, City: city };
    $.ajax({
        url:"/student/SaveStudent",
        method:"post",
        data: JSON.stringify(model),
        contentType:"application/json;charset=utf-8",
        dataType:"json",
        success:function (response) {
            alert("sucess");
           
        }
    });
}

var getmainstudent = function () {
    $.ajax({
        url:"/student/GetStudentList",
        method: "get",
        contentType: "application/Json;charset=utf-8",
        async: false,
        success: function (response) {
            var html = "";
            $("#tblstudent tbody").empty();
            $.each(response.model, function (index,elementvalue){
                html += "<tr><td>" + elementvalue.Id + "</td><td>" + elementvalue.StudentName + "</td><td>" + elementvalue.StudentAddress + "</td><td>" + elementvalue.City + "</td></tr>";
            });
            $("#tblstudent").append(html);
       }
    })
}


