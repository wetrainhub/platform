$(document).ready(function () {
    
    /// ---------------------------------------------------------
    /// Check all checkboxes in a column when the header checkbox is checked
    /// ---------------------------------------------------------
    $('.wth-table thead input:checkbox').on('change', function () {

        const currentTable = $(this).closest('table');

        // Find the index of the clicked header checkbox's column
        const columnIndex = $(this).closest('th').index();

        // Check if the header checkbox is checked or unchecked
        const isChecked = $(this).is(':checked');

        // Loop through each row in the table and check/uncheck the corresponding column's checkboxes
        currentTable.find('tbody tr').each(function () {
            $(this)
                .find('td')
                .eq(columnIndex)
                .find('input[type="checkbox"]')
                .prop('checked', isChecked);
        });
    });
});
