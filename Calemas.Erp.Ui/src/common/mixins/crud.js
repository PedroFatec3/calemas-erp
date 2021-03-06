import modal from 'vue-strap/src/Modal'
import pagination from 'vue-pagination-bootstrap'
import loading from '../loading'
import filterBase from './filter'

import { Api } from '../api'
import { Notification } from '../notification'
import { Form } from '../form'

export default {
    components: {
        modal,
        pagination
    },
    mixins: [ filterBase ],
    data() {
        return {

            resource: null,
            resources: {
                create: null,
                edit: null,
                delete: null,
                detail: null,
            },

            nameCreate: 'form-create',
            nameEdit: 'form-edit',
            nameDelete: 'form-delete',

            modalCreateIsOpen: false,
            modalEditIsOpen: false,
            modalDeleteIsOpen: false,
            modalDetailIsOpen: false,

            detail: null,
            model: {},
            modelEmpty: Object.assign({}, this.model, {}),

            notification: new Notification(this),
        }
    },
    computed: {
        apiCreate: function () {
            let resource = this.resource;
            if (this.resources.create) resource = this.resources.create;
            return new Api(resource);
        },
        apiEdit: function () {
            let resource = this.resource;
            if (this.resources.edit) resource = this.resources.edit;
            return new Api(resource);
        },
        apiDelete: function () {
            let resource = this.resource;
            if (this.resources.delete) resource = this.resources.delete;
            return new Api(resource);
        },
        apiDetail: function () {
            let resource = this.resource;
            if (this.resources.detail) resource = this.resources.detail;
            return new Api(resource);
        },
        formCreate: function () {
            let frm = this.nameCreate;
            return new Form(frm);
        },
        formEdit: function () {
            let frm = this.nameEdit;
            return new Form(frm);
        },
        formDelete: function () {
            let frm = this.nameDelete;
            return new Form(frm);
        },
    },
    methods: {

        openCreate: function (model) {
            this.resetForm(this.formCreate);
            if (model) this.model = model;
            else this.model = this.modelEmpty;
            this.modalCreateIsOpen = true;
            setTimeout(() => { this.formValid(this.formCreate); }, 500);

        },
        openEdit: function (id, item) {
            this.showLoading();
            this.resetForm(this.formEdit);
            this.apiEdit.filters = item;
            this.apiEdit.filters.id = id;
            this.apiEdit.get().then(data => {
                this.modalEditIsOpen = true;
                this.model = data.data;
                this.hideLoading();
                setTimeout(() => { this.formValid(this.formEdit); }, 500);
            }, () => { this.hideLoading(); });
        },
        openDelete: function (id, item) {
            this.showLoading();
            this.resetForm(this.formDelete);
            this.apiDelete.filters = item;
            this.apiDelete.filters.id = id;
            this.apiDelete.get().then(data => {
                this.modalDeleteIsOpen = true;
                this.model = data.data;
                this.hideLoading();
            }, () => { this.hideLoading(); });
        },
        openDetail: function (id, item) {
            this.apiDetail.filters = item;
            this.apiDetail.filters.id = id;
            this.apiDetail.get().then(data => {
                this.modalDetailIsOpen = true;
                if (data.dataList)
                    this.detail = data.dataList;
                else
                    this.detail = data.data;
            });
        },
        
        closeCreate: function () {
            this.modalCreateIsOpen = false;
        },
        closeEdit: function () {
            this.modalEditIsOpen = false;
        },
        closeDelete: function () {
            this.modalDeleteIsOpen = false;
        },
        closeDetail: function () {
            this.modalDetailIsOpen = false;
        },

        onBeforeCreate: (model) => { },
        onBeforeEdit: (model) => { },
        onBeforeDelete: (model) => { },

        onAfterCreate: (model) => { },
        onAfterEdit: (model) => { },
        onAfterDelete: (model) => { },

        executeCreate: function (model) {
            this.onBeforeCreate(model);

            if (this.formValid(this.formCreate) == false)
                return;

            this.defaultBeforeAction();
            this.apiCreate.post(model).then(data => {
                this.notification.success("Sucesso", "Criado com sucesso.")
                this.defaultSuccessResult(data);
                this.onAfterCreate(data);
                this.executeFilter();
                this.modalCreateIsOpen = false;
            }, err => {
                this.defaultErrorResult(err);
            })
        },
        executeEdit: function (model) {
            this.onBeforeEdit(model);

            if (this.formValid(this.formEdit) == false)
                return;

            this.defaultBeforeAction();
            this.apiEdit.post(model).then(data => {
                this.notification.success("Sucesso", "Edição realizada com sucesso.")
                this.defaultSuccessResult(data);
                this.onAfterEdit(data);
                this.executeFilter();
                this.modalEditIsOpen = false;
            }, err => {
                this.defaultErrorResult(err);
            })
        },
        executeDelete: function (model) {
            this.onBeforeDelete(model);

            if (this.formValid(this.formDelete) == false)
                return;

            this.defaultBeforeAction();
            this.apiDelete.filters = model;
            this.apiDelete.delete().then(data => {
                this.notification.success("Sucesso", "Removido com sucesso.")
                this.defaultSuccessResult(data);
                this.onAfterDelete(data);
                this.executeFilter();
                this.modalDeleteIsOpen = false;
            }, err => {
                this.defaultErrorResult(err);
            });
        },
                
        formCustom: function (frm) {
            return new Form(frm);
        },
        formValid: function (form) {
            return form.valid();
        },
        resetForm: function (form) {
            form.reset();
        },

        defaultBeforeAction: function () {
            this.showLoading();
        },
        defaultSuccessResult: function (res) {
            this.hideLoading();
        },
        defaultErrorResult: function (err) {
            this.hideLoading();

            if (err.data && err.data.result && err.data.result.errors)
                this.notification.error('Erro', err.data.result.errors[0]);
        },

        showLoading: function () {
            loading.show();
        },
        hideLoading: function () {
            loading.hide();
        },

    },

    mounted() {}
}
