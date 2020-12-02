class BaseJS {
  constructor() {
    this.host = "../api/v1/";
    this.initEvents();
    this.method = ""; //method là PUT hoặc POST ứng với kiểu sửa hoặc thêm entity
  }

  /**
   * Thêm các sự kiện xử lí
   *Created by: PTDuc(xx/11/2020)
   *Edited by: PTDuc(20/11/2020)
   */
  initEvents() {
    var self = this;
    //Load lại dữ liệu khi ấn nút sync
    $(".sync-m-btn").click(function () {
      self.loadData;
    });
    this.renderComboBox();
    //Ấn nút hủy trên thông báo xóa entity
    this.cancleDelete();

    //Ấn nút xác nhận xóa trên thông báo entity
    this.confirmDelete();

    //Hiển thị xác nhận xóa khi ấn vào xóa 1 dòng trên table
    this.deleteRow();

    //Hiển thị thùng rác ở mỗi row trên table -> ấn vào thùng rác thực hiện xóa dữ liệu entity ứng với row
    this.clickRowTable();

    // Sự kiện ấn nút thêm entity (customers,employees,...)
    this.addEntity();

    // ấn nút cancle hoặc ấn exit khỏi dialog thêm entity (customers,employees,...):
    this.hideDialog();

    // Thực hiện lưu dữ liệu khi nhấn button "Lưu " trên form chi tiết:
    this.saveFormAddData();

    // Hiển thị thông tin chi tiết khi nhấn đúp chuột chọn 1 bản ghi trên danh sách dữ liệu:
    this.doubleClickRow();

    //Kiểm tra các trường bắt buộc nhập khi out focus khỏi input
    this.checkRequired();

    //Kiểm tra email đúng định dạng khi out focus khỏi input
    this.checkEmail();
  }

  /**
   * Hiển thị thông báo
   * @param {string} content : Nội dung thông báo
   * @param {string} status : trạng thái thông báo
   */
  showNotification(content, status) {
    $(".notification").show();
    //Đảm bảo nếu có thông báo liên tục sẽ đè thông báo sau lên thông báo trước luôn
    $(".notification-content").empty();
    let notificationGroup = $(".notification-group");
    $(notificationGroup).empty();
    if (status === "success") {
      let notification = $(`<div class="notification notification-success">`);
      let icon = `<div class="notification-icon notification-icon-success"></div>`;
      let contentTitle = `<div class="notification-content">${content}</div>`;
      $(notification).append(icon);
      $(notification).append(contentTitle);
      $(notificationGroup).append(notification);
    } else if (status === "fail") {
      let notification = $(`<div class="notification notification-danger">`);
      let icon = `<div class="notification-icon notification-icon-danger"></div>`;
      let contentTitle = `<div class="notification-content">${content}</div>`;
      $(notification).append(icon);
      $(notification).append(contentTitle);
      $(notificationGroup).append(notification);
    }
  }

  /**
   * Kiểm tra email truyền vào có đúng định dạng không
   * @param {string} email : email cần kiểm tra, truyền vào dạng string
   */
  validateEmail(email) {
    var testEmail = /^[A-Z0-9._%+-]+@([A-Z0-9-]+\.)+[A-Z]{2,4}$/i;
    if (testEmail.test(email)) return true;
    return false;
  }
  /**
   * Sự kiện khi click vào dòng cần xóa, hiển thị thùng rác trên đầu
   * Created by: PTDuc(18/11/2020)
   */
  clickRowTable() {
    let garbage = $("#btnDelete");
    let self = this;
    $("table tbody").on("click", "tr", function () {
      $(garbage).show();
      self.deleteLink = $(this).data("EmployeeId");
    });
  }
  /**
   * Sự kiện khi ấn vào thùng rác, yêu cầu xóa dữ liệu của môt dòng- > hiển thị thông báo xác nhận xóa
   * Created by: PTDuc(18/11/2020)
   */
  deleteRow() {
    let self = this;
    $("#btnDelete").click(function (e) {
      console.log(self.deleteLink);
      $(".alert-delete").show();
    });
  }

  /**
   * Sự kiện ấn nút "hủy" trên thông báo yêu cầu xác nhận xóa dữ liệu
   * Created by: PTDuc(18/11/2020)
   */
  cancleDelete() {
    this.deleteLink = "";
    $("body").on("click", ".btn-cancle-alert,#btnCancleDelete", function () {
      $(".alert-delete").hide();
      $("#btnDelete").hide();
    });
  }

  /**
   * Sự kiện ấn nút "Xác nhận" trên thông báo yêu cầu xác nhận xóa dữ liệu
   * Created by: PTDuc(18/11/2020)
   */
  confirmDelete() {
    let self = this;
    //Gửi api yêu cầu xóa dữ liệu
    $("body").on("click", "#btnConfirmDelete", function () {
      $(".loading").show();
      $.ajax({
        url: self.host + self.router + "/" + self.deleteLink,
        method: "DELETE",
        contentType: "application/json",
      })
        .done(function (res) {
          $(".alert-delete").hide();
          //Xóa thành công sẽ hiển thị thông báo thành công
          self.showNotification("Xóa thành công", "success");
          self.deleteLink = "";
          //Load lại dữ liệu trên trang chủ
          self.loadData();
          $("#btnDelete").hide();
        })
        .fail(function (e) {
          $(".alert-delete").hide();
          //Xóa không thành công cũng sẽ hiển thị thông báo thất bại
          self.showNotification("Xóa thất bại", "fail");
          self.deleteLink = "";
          $("#btnDelete").hide();
        });
    });
  }

  /**
   * Load dữ liệu từ service cho các combobox
   * Created by: PTDuc(18/11/2020)
   */
  renderComboBox() {
    let self = this;
    //load các combobox

    var comboBoxs = $("select[notLoaded]");
    //show màn hình chờ cho người dùng biết đang load dữ liệu
    $(".loading").show();
    //Lấy dữ liệu nhóm khách hàng
    $.each(comboBoxs, function (index, comboBox) {
      let selectField = $(comboBox).attr("selectField");
      $(comboBox).empty();
      $.ajax({
        type: "GET",
        url: self.host + selectField,
      })
        .done((res) => {
          $.each(res, function (index, value) {
            let option = `<option ${selectField}Value="${
              value[selectField + "Id"]
            }" >${value[selectField + "Name"]}</option>`;
            $(comboBox).append(option);
          });
        })
        .fail((e) => {
          console.log(e);
        });
    });
    $(".loading").hide();
  }

  /**
   * Sự kiện ấn nút thêm entity (customers,employees,...)
   * Created by: PTDuc(20/11/2020)
   */
  addEntity() {
    let self = this;
    $(".add-user-m-btn").click(function () {
      self.method = "POST";
      try {
        // if (!$("#cbxCustomerGroup").children().length) self.renderComboBox();

        $(".include-content").show();
        $("[inputField]").val(null);
      } catch (e) {
        console.log(e);
      }
    });
  }

  /**
   * Sự kiện ấn nút cancle hoặc ấn exit khỏi dialog thêm entity (customers,employees,...)
   * Created by: PTDuc(20/11/2020)
   */
  hideDialog() {
    $("#btn-cancle-dialog, #btnCancel").click(function () {
      $(".include-content").hide();
    });
  }
  /**
   * Sự kiện ấn nút "Lưu" trên form thêm entity (customers,employees,...)
   * Created by: PTDuc(20/11/2020)
   */
  saveFormAddData() {
    let self = this;
    $("#btnSave").click(function () {
      var check = true; //kiểm tra các trường đã đủ và hợp lệ hay chưa, nếu có 1 trường bất kì chưa đạt yêu cầu gán bằng false ngay
      var entity = {};
      //lấy tất cả các input từ form HTML để build object và validate
      var allInputField = $("[inputField]");

      //validate dữ liệu
      $.each(allInputField, function (index, input) {
        if ($(input).prop("required")) {
          if (!$(input).val()) {
            $(input).addClass("border-red");
            self.showNotification("Chưa điền đủ các trường bắt buộc", "fail");
            check = false;
            //return false đảm bảo ngay khi phát hiện 1 trường nào thiếu giá trị thì thoát khỏi và báo luôn
            return false;
          }
        }
        if ($(input).attr("type") == "email") {
          if (!self.validateEmail($(input).val())) {
            alert("Định dạng email chưa chính xác");
            $(input).addClass("border-red");
            check = false;
            return false;
          }
        }

        // thu thập thông tin dữ liệu được nhập -> build thành object:
        if ($(input).attr("type") === "radio") {
          if ($(input).is(":checked")) {
            let inputField = $(input).attr("inputField");
            let value = $(input).attr("genderValue");
            entity[inputField] = value;
          }
        } else {
          let inputField = $(input).attr("inputField");
          let value = $(input).val();
          entity[inputField] = value;
        }
      });
      //Gọi API thêm dữ liệu lên server
      if (self.method === "PUT") {
        entity.EmployeeId = self.EmployeeId;
      }
      let url =
        self.host +
        self.router +
        "/" +
        (self.method == "PUT" ? self.EmployeeId : " ");
      debugger;
      //kiểm tra biến check rồi mới gửi request
      if (check) {
        $.ajax({
          url: url,
          method: self.method,
          data: JSON.stringify(entity),
          contentType: "application/json",
        })
          .done(function (res) {
            // Sau khi lưu thành công thì:
            // + ẩn form chi tiết,
            $(".include-content").hide();

            if (self.method === "PUT") {
              // + đưa ra thông báo thành công
              self.showNotification("Sửa thông tin thành công", "success");
            } else if (self.method === "POST") {
              // + đưa ra thông báo thành công
              self.showNotification("Thêm khách hàng thành công", "success");
            }

            // + load lại lại dữ liệu
            self.loadData();
          })
          .fail(function (e) {
            if (self.method === "PUT") {
              // + đưa ra thông báo thất bại
              self.showNotification("Sửa thông tin thất bại", "fail");
            } else if (self.method === "POST") {
              // + đưa ra thông báo thất bại
              self.showNotification("Thêm khách thất bại", "fail");
            }

            console.log(e);
          });
      }
    });
  }
  /**
   * Sự kiện ấn nhấn double click vào một dòng trên table -> hiển thị thông tin về entity
   * Created by: PTDuc(20/11/2020)
   */
  doubleClickRow() {
    let self = this;
    $("table tbody").on("dblclick", "tr", function () {
      self.method = "PUT";

      $("[inputField]").val(null);
      // if (!$("#cbxCustomerGroup").children().length) self.renderComboBox();
      $(".include-content").show();
      //Lấy id của bản ghi
      let id = $(this).data("EmployeeId");
      //gọi api lấy dữ liệu bản ghi
      $.ajax({
        type: "GET",
        url: self.host + self.router + "/" + id,
      })
        .done((res) => {
          self.EmployeeId = res.EmployeeId;
          //lấy tất cả các input từ form HTML
          var allInputField = $("[inputField]");
          $.each(allInputField, function (index, input) {
            //Riêng với trường hợp radio sẽ tách xử lí riêng
            if ($(input).attr("type") == "radio") {
              //Các trường nào quyết định sẽ hiển thị theo kiểu radio sẽ handle bên trong này
              //Trong trường hợp này chỉ có Gender muốn hiển thị theo kiểu radio nên chỉ bắt sự kiện
              //inputField là Gender
            } else if ($(input).attr("type") == "date") {
              //Với các trường kiểu date tách xử lí riêng, chỉ có date of birth nên handle ngay không cần kiểm tra tên trường nữa :))
              let inputField = $(input).attr("inputField");
              if (res[inputField]) {
                let day = new Date(res[inputField]);
              }
            } else if ($(input).attr("type") === "select") {
              let selectField = $(input).attr("selectField");
              let allOptions = $(this).find("option");
              let resField = $(this).attr("resField");
              $.each(allOptions, function (index, value) {
                let valueOfSelect = $(value).attr(selectField + "Value");
                let valueFromRes = res[resField];
                if (valueOfSelect.localeCompare(valueFromRes) === 0) {
                  $(value).prop("selected", true);
                } else {
                  $(value).prop("selected", false);
                }
              });
            } else {
              let inputField = $(input).attr("inputField");
              $(input).val(res[inputField]);
            }
          });
        })
        .fail((e) => {
          console.log(e);
        });
    });
  }
  /**
   * Kiểm tra việc nhập các trường bắt buộc trên form thêm hoặc sửa entity
   * Created by: PTDuc(20/11/2020)
   */
  checkRequired() {
    $("input[required]").blur(function () {
      // Kiểm tra dữ liệu đã nhập, nếu để trống thì cảnh báo:
      var value = $(this).val();
      if (!value) {
        $(this).addClass("border-red");
        $(this).attr("title", "Trường này không được phép để trống");
      } else {
        $(this).removeClass("border-red");
      }
    });
  }
  /**
   *Kiểm tra các ô type email có đúng định dạng email
   *Created by: PTDuc(xx/11/2020)
   *Edited by: PTDuc(20/11/2020)
   */
  checkEmail() {
    let self = this;
    $('input[type="email"]').blur(function () {
      var value = $(this).val();
      if (!self.validateEmail(value)) {
        $(this).addClass("border-red");
        $(this).attr("title", "Email không đúng định dạng.");
      } else {
        $(this).removeClass("border-red");
      }
    });
  }

  /**
   * Đưa ngày tháng ra theo định dạng
   * @param {string} date : date truyền vào là 1 string
   * @param {string} type: type là kiểu định dạng ngày tháng trả về VD:ddmmyyyy,..
   */

  formatDate(data, type) {
    let date = new Date(data);
    let day = date.getDate() < 10 ? "0" + date.getDate() : date.getDate();
    let month =
      date.getMonth() + 1 < 10
        ? "0" + (date.getMonth() + 1)
        : date.getMonth() + 1;
    let year = date.getFullYear();
    switch (type) {
      case "ddmmyyyy":
        return day + "/" + month + "/" + year;
        break;
      case "mmddyyyy":
        return month + "/" + day + "/" + year;
        break;
      default:
        return "";
    }
  }

  /**
   * Format lại data thành dạng tiền tệ
   * @param {string} data : data là giá trị cần định dạng tiền tệ
   */
  formatMoney(data) {
    return data.toFixed(2).replace(/\d(?=(\d{3})+\.)/g, "$&,"); // 12,345.67
  }
  loadData() {
    //show màn hình chờ cho người dùng biết đang load dữ liệu
    $(".loading").show();
    //Lấy thông tin các cột dữ liệu
    try {
      var ths = $("table thead th");
      var fieldName = [];
      var self = this;
      $(".table-content").empty();
      $.each(ths, function (index, value) {
        fieldName.push();
      });
      //Map dữ liệu lên UI
      $.ajax({
        type: "GET",
        url: self.host + self.router,
        async: false,
      }).done((res) => {
        $.each(res, function (index, value) {
          //value là một object employee trả về
          let tr = $("<tr></tr>");
          $(tr).data("EmployeeId", value.EmployeeId);
          $.each(ths, (ind, val) => {
            //duyệt qua hết các tất cả các cột để lấy các thuộc tính tương ứng
            var fieldName = $(val).attr("fieldName");
            var typeFormat = $(val).attr("formatType");
            var text_align = $(val).attr("class");
            var data = value[fieldName];
            if (data) {
              if (typeFormat === "ddmmyyyy" || typeFormat === "mmddyyyy") {
                data = self.formatDate(data, typeFormat);
              } else if (typeFormat === "money") {
                data = self.formatMoney(data);
              }
            } else {
              data = "";
            }
            var td = `<td class="${text_align}">${data}</td>`;
            $(tr).append(td);
            $(".table-content").first().append(tr);
            $(".table-content").first().append(tr);
          });

          $(".loading").hide();
        });
      });
    } catch (error) {
      console.log(error);
    }
  }
  formatDateCalendar(day, type) {
    if (type === "mmddyyyy") {
      let date = day.getDate();
      let month = day.getMonth();
      let year = day.getFullYear();
      if (date < 10) date = "0" + date;
      if (month + 1 < 10) month = "0" + (month + 1);
      $(input).val(year + "-" + month + "-" + date);
    }
  }
}
