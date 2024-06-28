$('document').ready(() => {
    $('#hd-check').on('change', () => {
        var isChecked = $('#hd-check').is(':checked');
        $('#hd-check').val(isChecked);
        if (isChecked)
            $('#hd-price').prop('disabled', false);
        else
            $('#hd-price').val('').prop('disabled', true);
    });

    $('#hdf-check').on('change', () => {
        var isChecked = $('#hdf-check').is(':checked');
        $('#hdf-check').val(isChecked);
        if (isChecked)
            $('#hdf-price').prop('disabled', false);
        else
            $('#hdf-price').val('').prop('disabled', true);
    });


    var fileArray = [];

    // Trigger file input click on upload button click
    $('#upload-btn').on('click', function () {
        $('#image-input').click();
    });


    // Handle file input change event
    $('#image-input').on('change', function () {
        var files = this.files;
        var imageContainer = $('.image-container');

        for (var i = 0; i < files.length; i++) {
            var file = files[i];
            fileArray.push(file);

            (function (index) {
                var reader = new FileReader();

                reader.onload = function (e) {
                    var imgHtml = `
                            <div class="image shadow">
                                <img src="${e.target.result}" width="250px" height="250px" />
                                <button class="img-del" data-idx="${index}">
                                    <i class="fa fa-close"></i>
                                </button>
                                <div style="margin-top:5px;">
                                    <input type="radio" name="thumbnail" data-idx="${index}" value="${index}" />
                                    <label>Thumbnail</label>
                                </div>
                            </div>
                        `;
                    imageContainer.append(imgHtml);
                };

                reader.readAsDataURL(file);
            })(i);
        }
    });

    // Handle delete button click event
    $(document).on('click', '.img-del', function () {
        var index = $(this).attr('data-idx');
        fileArray.splice(index, 1);
        $(this).closest('.image').remove();

        // Update the file input with the remaining files
        var dataTransfer = new DataTransfer();
        fileArray.forEach(function (file) {
            dataTransfer.items.add(file);
        });
        $('#image-input')[0].files = dataTransfer.files;

        // Update the indexes of the remaining images and thumbnails
        $('.image-container .image').each(function() {
            var i = $(this).find('.img-del').attr('data-idx');
            if (i > index) {
                $(this).find('.img-del').attr('data-idx', i - 1);
                $(this).find('input[name="thumbnail"]').attr('data-idx', i - 1);
                $(this).find('input[name="thumbnail"]').val(i - 1);
            }
        });
    });

});