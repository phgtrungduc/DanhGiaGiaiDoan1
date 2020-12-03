$(document).ready(function () {
  new EmployeeJS();
  $(".loading").show();
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

  /**
   * Chuyển từ dữ liệu hiển thị sang dạng dữ liệu sẽ gửi lên server
   * @param {*} fieldName : Tên trường
   * @param {*} data : Giá trị của trường
   * createdBy:PTDuc(04/12/2020)
   */
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

  /**
   * Thực hiện format lại kiểu của một số trường không tồn tại tại base
   * @param {string} fieldName :Tên trường cần format lại
   * @param {*} data : giá trị của trường
   * createdBy:PTDuc(04/12/2020)
   */
  customFormat(fieldName, data) {
    if (fieldName === "Gender" || fieldName === "WorkStatus") {
      return this.mappingData(fieldName, data);
    } else return data;
  }
  /**
   * Sự kiện khi ấn vào nút thêm mới 
   * createdBy:PTDuc(04/12/2020)
   */
  addEntity() {
    this.getMaxEmployeeCode();
    super.addEntity();
  }
  /**
   * Lấy mã của nhân viên có mã lớn nhất hệ thống
   * createdBy:PTDuc(04/12/2020)
   */
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

  /**
   * Format cho đúng chuẩn định dạng mã nhân viên
   * @param {string} employeeCode : mã nhân viên có mã lớn nhất lấy từ hệ thống
   * Trả lại giá trị NV+(phần số +1)
   * createdBy:PTDuc(04/12/2020)
   */
  formatEmployeeCode(employeeCode) {
    let number = employeeCode.substring(2);
    number = parseInt(number);
    number++;
    let fillNumber = number.toString().padStart(6, "0");
    return "NV" + fillNumber;
  }
  
  /**
   * Khởi tạo các sự kiện khi thay đổi các trường để filter
   * createdBy:PTDuc(04/12/2020)
   */
  initFilter(){
    let self = this;
    $("cbxPosition").change(function(){self.filterEmployee()});
    $("cbxDepartment").change(function(){self.filterEmployee()});
    $("input.search-another").blur(function(){self.filterEmployee()});
    
  }
  /**
   * Thực hiện tìm kiếm 
   * createdBy:PTDuc(04/12/2020)
   */
  filterEmployee() {
    let self = this;
    let specs = $("input.search-another").val().trim();
    let selectPosition = $("#cbxPosition");
    let optionPosition = $(selectPosition).find("option:selected");
    let valuePosition = $(optionPosition).attr("positionValue");

    let selectDepartment = $("#cbxDepartment");
    let optionDepartment = $(selectDepartment).find("option:selected");
    let valueDepartment = $(optionDepartment).attr("departmentValue");
    this.param = `/filter?specs=${specs}&positionid=${valuePosition}&departmentId=${valueDepartment}`;
    this.loadData();
  }
}
