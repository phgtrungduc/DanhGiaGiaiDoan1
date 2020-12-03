$(document).ready(function () {
  new EmployeeJS();
  $(".loading").show();
  $(function () {
    $("#datepicker").datepicker();
  });
});

class EmployeeJS extends BaseJS {
  constructor() {
    super();
    this.router = "employee";
    this.loadData();
  }
  initEvents() {
    super.initEvents();
    this.initFilter();
  }
  mappingData(fieldName, data) {
    if (fieldName === "Gender") {
      if (data === 0) {
        return "Nữ";
      } else if (data === 1) {
        return "Nam";
      } else {
        return "Khác";
      }
    }
    if (fieldName === "WorkStatus") {
      if (data === 0) {
        return "Đang làm việc";
      } else if (data === 1) {
        return "Đang thử việc";
      } else {
        return "Đã nghỉ việc";
      }
    }
  }
  customFormat(fieldName, data) {
    if (fieldName === "Gender" || fieldName === "WorkStatus") {
      return this.mappingData(fieldName, data);
    } else return data;
  }
  addEntity() {
    this.getMaxEmployeeCode();
    super.addEntity();
  }
  getMaxEmployeeCode() {
    let self = this;
    $.ajax({
      type: "GET",
      url: self.host + "employee/filter/maxcode",
      success: function (response) {
        self.maxcode = self.formatEmployeeCode(response);
      },
    });
  }
  formatEmployeeCode(employeeCode) {
    let number = employeeCode.substring(2);
    number = parseInt(number);
    number++;
    let fillNumber = number.toString().padStart(6, "0");
    return "NV" + fillNumber;
  }
  
  initFilter(){
    let self = this;
    $("select[selectfield=Department]").change(function(){self.filterEmployee()});
    $("select[selectfield=Position]").change(function(){self.filterEmployee()});
    $("input.search-another").blur(function(){self.filterEmployee()});
    
  }
  filterEmployee() {
    let self = this;
    let specs = $("input.search-another").val().trim();
    let selectPosition = $("select[selectfield=Position]");
    let optionPosition = $(selectPosition).find("option:selected");
    let valuePosition = $(optionPosition).attr("positionValue");

    let selectDepartment = $("select[selectfield=Department]");
    let optionDepartment = $(selectDepartment).find("option:selected");
    let valueDepartment = $(optionDepartment).attr("departmentValue");

    this.param = `/filter?specs=${specs}&positionid=${valuePosition}&departmentId=${valueDepartment}`;
    debugger
    this.loadData();
  }
}
