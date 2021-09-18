var toastJs = {
    showToastMessage(toastType, toastTitle, toastMessage) {
		toastr.options = {
			closeButton: true,
			debug: false,
			newestOnTop: true,
			progressBar: true,
			rtl: false,
			positionClass: 'toast-top-right',
			preventDuplicates: true,
			onclick: null
		};

		if (toastType === 'info') {
			toastr.info(toastMessage, toastTitle);
        }

		if (toastType === 'success') {
			toastr.success(toastMessage, toastTitle);
		}

		if (toastType === 'warning') {
			toastr.warning(toastMessage, toastTitle);
		}

		if (toastType === 'error') {
			toastr.error(toastMessage, toastTitle);
        }
    }
}