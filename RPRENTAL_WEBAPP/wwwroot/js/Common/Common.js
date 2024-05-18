﻿$(document).ready(function () {
	
	if (localStorage.getItem('loginTriggered')) {		
		ShowToaster('success', 'LOGIN USER', localStorage.getItem('loginMsg'));
		localStorage.removeItem('loginTriggered');
		localStorage.removeItem('loginMsg');
		
	}

	if (localStorage.getItem('logoutTriggered')) {		
		ShowToaster('success', 'LOGOUT USER', localStorage.getItem('logoutMsg'));
		localStorage.removeItem('logoutTriggered');
		localStorage.removeItem('logoutMsg');
	
	}
});

function ShowToaster(Type, Header, Message)
{  

	toastr.options.hideDuration = 2000;
	toastr.options.preventDuplicates = 1;
	toastr.options.closeButton = 1;
	toastr.options.positionClass = 'toast-bottom-right';

	
	switch (Type)
	{	
		case 'success':
			toastr.success(Message, Header);
			break;
		case 'error':
			toastr.error(Message, Header);
			break;
		case 'info':
			toastr.info(Message, Header);
			break;
		case 'warning':
			toastr.warning(Message, Header);
			break;		
	}	
}

function DisplayImagePreview(event) {
	let imageSrc = URL.createObjectURL(event.target.files[0]);
	$('#image_preview').attr('src', imageSrc);
}


function ReloadDataTable(objMainTable) {
	objMainTable.ajax.reload();
}

function LoadModal(url, modalContent, data = null) {

	debugger

	$.ajax({
		type: 'GET',
		url: url,
		data: data,
		success: function (response) {			
			if (response.success) {	
				$(modalContent).html('');	
				$(modalContent).empty();				
				$(modalContent).html(response.htmlContent);
				$(modalContent.replace('-content', '')).modal('show');
			}
			else {
				$(modalContent).html('');
				$(modalContent).empty();	
				ShowToaster('error', '', response.message);
			}		
		},
		error: function (xhr, status, error) {
			$(modalContent).html('');
			$(modalContent).empty();	
			ShowToaster('error', 'Error', error);
		}
	});

	$(document).on('hidden.bs.modal', modalContent.replace('-content', ''), function () {
		$(modalContent).html('');
	});
}

function CloseModal(modal_id) {
	$(modal_id).html('');
	$(modal_id).empty();
}



function HideModal(modalContent) {		
	$(modalContent).modal('hide');
}

function InputBoxFocus(input_name, modal_name) {		
	$(document).on('shown.bs.modal', modal_name, function () {

		var input = $(input_name);
		input.focus().select();

		if (input.val() !== '') {
			var inputLength = input.val().length;
			input[0].setSelectionRange(inputLength, inputLength);
		}
	});
}

function IsFieldValid(formSelector) {	
	
	if (!$(formSelector)[0].checkValidity()) {
		$(formSelector).addClass('was-validated');
		return false;
	}


	return true;
}

function ValidateEmail(email) {	
	
	let is_valid = false;	
	let emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
	is_valid = emailPattern.test(email);

	if (!is_valid) {
		$('#email-validation').css('color', 'red').html('Please enter a valid email.');
		return;
	}
}

function GetRowData(objTable, btn) {	
	return objTable.row(btn.closest('tr')).data();
}



