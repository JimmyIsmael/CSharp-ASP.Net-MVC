//$('#printInvoice').click(function () {
//    Popup($('.invoice')[0].outerHTML);
//    function Popup(data) {
//        window.print();
//        return true;
//    }
//});

function printInvoice() {
    $("#invoice").printThis();

    return true;
}