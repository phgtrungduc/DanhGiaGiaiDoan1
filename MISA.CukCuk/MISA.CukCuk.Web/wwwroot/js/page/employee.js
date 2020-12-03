
$(document).ready(function () {
  new EmployeeJS();
  $( function() {
    $( "#datepicker" ).datepicker();
  } );
});

class EmployeeJS extends BaseJS {
  constructor() {
    super();
    this.router = "employee";
    this.searchValue="";
    this.loadData();
  }
  initEvents(){
    super.initEvents();
    this.filterByPosition();
    this.filterByDepartment();
    this.filterByAnother();
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
  addEntity(){
    this.getMaxEmployeeCode();
    super.addEntity();
  }
  getMaxEmployeeCode(){
    let self = this;
    $.ajax({
      type: "GET",
      url: self.host+"employee/a/b/c/maxcode",
      success: function (response) {
        self.maxcode = self.formatEmployeeCode(response);
      }
    });
  }
  formatEmployeeCode(employeeCode){
    let number = employeeCode.substring(2);
    number = parseInt(number);
    number++;
    let fillNumber = number.toString().padStart(6,'0');
    return "NV"+fillNumber;
  }
  filterByPosition(){
    let self = this;
    $("select[selectfield=Position]").change(function() { 
      let option = $(this).find("option:selected");
      let resField = $(this).attr("resField");
      let value = $(option).attr("positionValue");
      if (value==="all"){
        self.param="";
        self.loadData();
      }
      else {
        self.param = `?position=${value}`;
        self.loadData();
      }
    });
  }
  filterByDepartment(){
    let self = this;
    $("select[selectfield=Department]").change(function() { 
      let option = $(this).find("option:selected");
      let resField = $(this).attr("resField");
      let value = $(option).attr("departmentValue");
      if (value==="all"){
        self.param="";
        self.loadData();
      }
      else {
        self.param = `?department=${value}`;
        self.loadData();
      }
    });
  }
  filterByAnother(){
    let self = this;
    $("input.search-another").blur(function(){ 
      let value = $(this).val().trim();
      if (self.searchValue!==value){
        if (value==null||value===""){
          self.param="";
          
          self.loadData();
        }
        else {
          self.param=`/search/${value}`;
          self.loadData();
        }
      }
      
    });
  }
}
