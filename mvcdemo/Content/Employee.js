$(document).ready(function () {
    getmainstudent();
});

var getmainstudent = function () {
    $.ajax({
        url: "/Employee/GetEmpList",
        method: "get",
        contentType: "application/Json;charset=utf-8",
        async: false,
        success: function (response) {
            var html = "";
            $("#tblemployee tbody").empty();
            $.each(response.model, function (index, elementvalue) {
                html += "<tr><td>" + elementvalue.EmpId + "</td><td>" + elementvalue.EmpName + "</td><td>" + elementvalue.EmpAddress + "</td><td>" + elementvalue.EmpSalary + "</td><td>" + elementvalue.Job + "</td></tr>";
            });
            $("#tblemployee").append(html);
        }
    })
}