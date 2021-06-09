import FixedSalaryEmployee from './Employees';
import HourlySalaryEmployee from './Employees';
import EmployeesCollection from './EmployeeCollection';
import _ from 'lodash-es'
import $ from 'jquery'

var employeesCollection = new EmployeesCollection();

function mapToEmployee(employee) {
    var emp = employee.type == 'HourlySalaryEmployee'
        ? new HourlySalaryEmployee(employee.id, employee.name, employee.salary)
        : new FixedSalaryEmployee(employee.id, employee.name, employee.salary);
    return emp;
}

$('#load-from-source').on('click', function () {
    employeesCollection = new EmployeesCollection(_.flatMap(//data//, mapToEmployee));
    fillTable(employeesCollection.employees);
})

$('#load-from-textarea').on('click', function () {
    try {
        employeesCollection = new EmployeesCollection(
            _.flatMap(
                JSON.parse($('#input-data').val()),
                mapToEmployee
            )
        );
        fillTable(employeesCollection.employees);
    } catch {
        showErrorSection();
    }
});

function showErrorSection() {
   console.log("error")
}

function fillTable(employees) {
    var rows = _.flatMap(employees, function (employee) {
        var text = '<tr><td>'
        + employee.id+ '</td><td>'
        + employee.name + '</td><td>'
        + employee.getSalary() + '</td></tr>'
        return text;
    });
    
    $('#data-table > tbody').empty().append(rows);
}

