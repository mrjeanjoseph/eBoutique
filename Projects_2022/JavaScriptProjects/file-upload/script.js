
Array.prototype.forEach.call(document.querySelectorAll('.file-upload__button'), function(button){
    const hiddenInput = button.parentElement.querySelector('.file-upload__input');
    const label = button.parentElement.querySelector('.file-upload__label');

    const defaultLabelText = 'No file(s) selected';

    //Set default text for label
    label.textContent = defaultLabelText;
    label.title = defaultLabelText;

    button.addEventListener('click', function() {
        hiddenInput.click();
    });

    hiddenInput.addEventListener('change', function () {
        const fileNameList = Array.prototype.map.call(hiddenInput.files, function(file) {
            return file.name;
        });

        label.textContent = fileNameList.join(', ') || defaultLabelText;
        label.title = label.textContent;
    })
});