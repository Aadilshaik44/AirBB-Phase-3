// Add method to jQuery validator
jQuery.validator.addMethod("builtyear", function (value, element, param) {
    if (value === "") return true; // Let required validator handle empty
    
    var year = parseInt(value);
    var currentYear = new Date().getFullYear();
    var maxAge = parseInt(param);
    var minYear = currentYear - maxAge;

    // Check if valid number, not in future, and not too old
    return !isNaN(year) && year <= currentYear && year >= minYear;
});

// Add adapter for unobtrusive validation
jQuery.validator.unobtrusive.adapters.addSingleVal("builtyear", "maxyears");