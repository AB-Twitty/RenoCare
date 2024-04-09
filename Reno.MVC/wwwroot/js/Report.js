let menuLink = document.querySelectorAll('.ibox-content ul li');
let formUl = document.querySelector('.ibox .ibox-content .Report-body form')
let box = [
    `
    <div class="row">
        <div class="col-lg-6 col-md-12">
            <div class="form-group">
                <label class="control-label" for="Visit">Type Of Visit</label>
                <select class="m-b form-control" disabled="disabled">
                    <option value="3">Unplanned Follow-up visit</option>
                    </select>
            </div>
        </div>
        <div class="col-lg-6 col-md-12 mt-lg-0 mt-md-2">
            <div class="form-group">
                <label class="control-label" for="DataVisit">Date Of Visit <span class=" text-danger">*</span></label>
                <input type="text" class="m-b form-control datepicker" value="17-Jan-2024" disabled="disabled">
            </div>
        </div>
        <div class="col-lg-6 col-md-12 mt-lg-4 mt-md-2">
            <div class="form-group">
                <label class="control-label" for="FormCompleted">Form Completed By <span class=" text-danger">*</span></label>
                <input type="text" class="m-b form-control datepicker" disabled="disabled">
            </div>
        </div>
    </div>
    <div class="AllButtons d-flex justify-content-end mt-4">
        <button id="nxt_to_clinical" class="next btn btn-primary">Next</button>
    </div>
    `,
    `
    <div class="row">
                                    <div class="col-lg-6 col-md-12">
                                        <div class="form-group">
                                            <label class="control-label" for="Visit">Gender <span class=" text-danger">*</span></label>
                                            <div class="row">
                                                <div class="radio col-md-4">
                                                    <div class="iradio_square-green disabled checked" style="position: relative;">
                                                        <input type="radio" value="Male" class="i-checks isMale BaselineSingleControl" checked="checked" id="Gender" name="Gender" style="position: absolute; opacity: 0;">
                                                        <ins class="iCheck-helper"
                                                        style="position: absolute; top: 0%; left: 0%; display: block; width: 100%; height: 100%; margin: 0px; padding: 0px; background: rgb(255, 255, 255); border: 0px; opacity: 0; text-decoration: none;">
                                                        </ins>
                                                    </div>
                                                    Male
                                                </div>
                                                <div class="radio col-md-4 p-0">
                                                    <div class="iradio_square-green disabled" style="position: relative;">
                                                        <input type="radio" value="Male" class="i-checks isMale BaselineSingleControl" checked="checked" id="Gender" name="Gender" style="position: absolute; opacity: 0;">
                                                        <ins class="iCheck-helper"
                                                        style="position: absolute; top: 0%; left: 0%; display: block; width: 100%; height: 100%; margin: 0px; padding: 0px; background: rgb(255, 255, 255); border: 0px; opacity: 0; text-decoration: none;">
                                                        </ins>
                                                    </div>
                                                    Female
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-6 col-md-12 mt-lg-0 mt-md-2">
                                        <div class="form-group">
                                            <label class="control-label" for="DataVisit">Date Of Birth <span class=" text-danger">*</span></label>
                                            <input type="text" class="m-b form-control datepicker" value="03-Jan-2024" disabled="disabled">
                                        </div>
                                    </div>
                                    <div class="col-lg-6 col-md-12  mt-lg-2 mt-md-2">
                                        <div class="form-group">
                                            <label class="control-label" for="Age">Age</label>
                                            <select class="m-b form-control appearance" disabled="disabled">
                                                <option value="3">0 Y 0 M</option>
                                            </select>
                                        </div>
                                    </div>
                                    <div class="col-lg-6 col-md-12 mt-lg-2 mt-md-2">
                                        <div class="form-group">
                                            <label class="control-label" for="Visit">Ethnic Origin</label>
                                            <select class="m-b form-control" disabled="disabled">
                                                <option value="3">Select Ethinic Orgin</option>
                                                </select>
                                        </div>
                                    </div>
                                    <div class="col-lg-6 col-md-12 mt-lg-2 mt-md-2">
                                        <div class="form-group">
                                            <label class="control-label" for="Visit">Nationality <span class=" text-danger">*</span></label>
                                            <select class="m-b form-control" disabled="disabled">
                                                <option value="3">Saudi</option>
                                                </select>
                                        </div>
                                    </div>
                                    <div class="col-lg-6 col-md-12 mt-lg-2 mt-md-2">
                                        <div class="form-group">
                                            <label class="control-label" for="Visit">Country Of Birth <span class=" text-danger">*</span></label>
                                            <select class="m-b form-control" disabled="disabled">
                                                <option value="3">Austria</option>
                                                </select>
                                        </div>
                                    </div>
                                    <div class="col-lg-6 col-md-12 mt-lg-2 mt-md-2">
                                        <div class="form-group">
                                            <label class="control-label" for="Visit">Country Of Residence <span class=" text-danger">*</span></label>
                                            <select class="m-b form-control" disabled="disabled">
                                                <option value="3">Saudi Arabia</option>
                                                </select>
                                        </div>
                                    </div>
                                    <div class="col-lg-6 col-md-12 mt-lg-2 mt-md-2">
                                        <div class="form-group">
                                            <label class="control-label" for="Visit">Marital Status</label>
                                            <select class="m-b form-control" disabled="disabled">
                                                <option value="3">Single</option>
                                                </select>
                                        </div>
                                    </div>
                                    <div class="col-lg-6 col-md-12 mt-lg-2 mt-md-2">
                                        <div class="form-group">
                                            <label class="control-label" for="Visit">City Of Residence <span class=" text-danger">*</span></label>
                                            <select class="m-b form-control" disabled="disabled">
                                                <option value="3">Abqaiq / بقبق</option>
                                                </select>
                                        </div>
                                    </div>
                                    <div class="col-lg-6 col-md-12 mt-lg-2 mt-md-2">
                                        <div class="form-group d-flex flex-column h-100 justify-content-end">
                                            <label class="control-label" for="Visit">Region<span class=" text-danger">*</span></label>
                                            <label class="control-label" for="Visit">East</label>
                                            
                                        </div>
                                    </div>
                                    <div class="col-lg-6 col-md-12 mt-lg-2 mt-md-2">
                                        <div class="form-group">
                                            <label class="control-label" for="Visit">Educational Level</label>
                                            <select class="m-b form-control" disabled="disabled">
                                                <option value="3">Less than high school (With Drawn From school due to NMD)</option>
                                                </select>
                                        </div>
                                    </div>
                                    <div class="col-lg-6 col-md-12 mt-lg-2 mt-md-2">
                                        <div class="form-group">
                                            <label class="control-label" for="Visit">Employment</label>
                                            <select class="m-b form-control" disabled="disabled">
                                                <option value="3">Student</option>
                                                </select>
                                        </div>
                                    </div>
                                    <div class="col-lg-6 col-md-12 mt-lg-2 mt-md-2">
                                        <div class="form-group">
                                            <label class="control-label" for="Visit">Smoking Status</label>
                                            <select class="m-b form-control" disabled="disabled">
                                                <option value="3">Select Smoking Status</option>
                                                </select>
                                        </div>
                                    </div>
                                </div>
                                <div class="AllButtons d-flex justify-content-between mt-4">
                                    <button id="prev_to_visit" class="previous btn btn-secondary">Previous</button>
                                    <button id="nxt_to_clinical" class="next btn btn-primary">Next</button>
                                </div>
    `,
    `
    <div class="row">
        <div class="col-lg-6 col-md-12">
            <div class="form-group">
                <label class="control-label" for="DataVisit">Weight <span class="font-12"> - Kg</span></label>
                <input type="text" class="m-b form-control datepicker" value="" disabled="disabled">
            </div>
        </div>
        <div class="col-lg-6 col-md-12">
            <div class="form-group">
                <label class="control-label" for="DataVisit">Height <span class="font-12"> - cm</span></label>
                <input type="text" class="m-b form-control datepicker" value="" disabled="disabled">
            </div>
        </div>
        <div class="col-lg-6 col-md-12 mt-lg-0 mt-md-2">
            <div class="form-group">
                <label class="control-label" for="DataVisit">Body Mass Index (BMI) <span class="font-12"> - Kg/M2</span></label>
                <input type="text" class="m-b form-control datepicker" value="" disabled="disabled">
            </div>
        </div>
        <div class="col-lg-6 col-md-12 mt-lg-0 mt-md-2">
            <div class="form-group">
                <label class="control-label" for="DataVisit">Pulse</label>
                <input type="text" class="m-b form-control datepicker" value="" disabled="disabled">
            </div>
        </div>
        <div class="col-lg-6 col-md-12 mt-lg-0 mt-md-2">
            <div class="form-group">
                <label class="control-label" for="DataVisit">Blood Pressure Systolic <span class="font-12"> - mmHG</span></label>
                <input type="text" class="m-b form-control datepicker" value="" disabled="disabled">
            </div>
        </div>
        <div class="col-lg-6 col-md-12 mt-lg-0 mt-md-2">
            <div class="form-group">
                <label class="control-label" for="DataVisit">Blood Pressure Diastolic <span class="font-12"> - mmHG</span></label>
                <input type="text" class="m-b form-control datepicker" value="" disabled="disabled">
            </div>
        </div>
    </div>
    <div class="AllButtons d-flex justify-content-between mt-4">
        <button id="prev_to_visit" class="previous btn btn-secondary">Previous</button>
        <button id="nxt_to_clinical" class="next btn btn-primary">Next</button>
    </div>
    `,
    `
    <div class="row">
        <div class="col-lg-6 col-md-12">
            <div class="form-group">
                <label class="control-label" for="Visit">Type Of Visit</label>
                <select class="m-b form-control" disabled="disabled">
                    <option value="3">Unplanned Follow-up visit</option>
                    </select>
            </div>
        </div>
        <div class="col-lg-6 col-md-12 mt-lg-0 mt-md-2">
            <div class="form-group">
                <label class="control-label" for="DataVisit">Date Of Visit <span class=" text-danger">*</span></label>
                <input type="text" class="m-b form-control datepicker" value="17-Jan-2024" disabled="disabled">
            </div>
        </div>
        <div class="col-lg-6 col-md-12 mt-lg-4 mt-md-2">
            <div class="form-group">
                <label class="control-label" for="FormCompleted">Form Completed By <span class=" text-danger">*</span></label>
                <input type="text" class="m-b form-control datepicker" disabled="disabled">
            </div>
        </div>
    </div>
    <div class="AllButtons mt-4">
    <button id="prev_to_visit" class="previous btn btn-secondary">Previous</button>
    </div>
    `
]

for (let i = 0; i < menuLink.length; i++) {
    menuLink[i].addEventListener("click", ()=>{
        menuLink.forEach(link => link.classList.remove("active"));
        menuLink[i].classList.add("active");
        formUl.innerHTML = box[i]
    })
    
}

