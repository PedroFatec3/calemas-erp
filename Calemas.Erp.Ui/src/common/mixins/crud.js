﻿import modal from 'vue-strap/src/Modal'
import pagination from 'vue-pagination-bootstrap'
import bus from '../../common/bus'
import { Api } from '../api'

export default {
    components: {
        modal,
        pagination
    },
    data() {
        return {

            resource: null,
            resources: {
                create: null,
                edit: null,
                delete: null,
                detail: null,
                filter: null
            },

            modalCreateOpen: false,
            modalEditOpen: false,
            modalDeleteOpen: false,
            modalDetailOpen: false,

            model: {},

            modelEmpty: Object.assign({}, this.model, {}),

            filter: {
                pageSize: 10,
                pageIndex: 1,
                isPaginate: true,
                queryOptimizerBehavior: "",
            },
            result: {
                total: 0,
                itens: []
            }
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
        apiFilter: function () {
            let resource = this.resource;
            if (this.resources.filter) resource = this.resources.filter;
            return new Api(resource);
        },
    },
    methods: {

        openCreate: function (model) {
            this.resetForm();
            if (model) this.model = model;
            else this.model = this.modelEmpty;
            this.modalCreateOpen = true;
        },
        openEdit: function (id, item) {
            this.resetForm();
            this.apiEdit.filters = item;
            this.apiEdit.filters.id = id;
            this.apiEdit.get().then(data => {
                this.modalEditOpen = true;
                this.model = data.data;
            });
        },
        openDelete: function (id, item) {
            this.resetForm();
            this.apiDelete.filters = item;
            this.apiDelete.filters.id = id;
            this.apiDelete.get().then(data => {
                this.modalDeleteOpen = true;
                this.model = data.data;
            });
        },
        openDetail: function (id, item) {
            this.apiDetail.filters = item;
            this.apiDetail.filters.id = id;
            this.apiDetail.get().then(data => {
                this.modalDetailOpen = true;
                this.model = data.data;
            });
        },

        closeCreate: function () {
            this.modalCreateOpen = false;
        },
        closeEdit: function () {
            this.modalEditOpen = false;
        },
        closeDelete: function () {
            this.modalDeleteOpen = false;
        },
        closeDetail: function () {
            this.modalDetailOpen = false;
        },

        onBeforeCreate: (model) => { },
        onBeforeEdit: (model) => { },
        onBeforeDelete: (model) => { },

        onAfterCreate: (model) => { },
        onAfterEdit: (model) => { },
        onAfterDelete: (model) => { },

        executeCreate: function (model) {
            this.onBeforeCreate(model);
            this.formValidate(() => {
                this.apiCreate.post(model).then(data => {
                    this.onAfterCreate(data);
                    this.executeFilter();
                    this.modalCreateOpen = false;
                }, err => { })
            });
        },
        executeEdit: function (model) {
            this.onBeforeEdit(model);
            this.formValidate(() => {
                this.apiEdit.post(model).then(data => {
                    this.onAfterEdit(data);
                    this.executeFilter();
                    this.modalEditOpen = false;
                }, err => { })
            });
        },
        executeDelete: function (model) {
            this.onBeforeDelete(model);
            this.formValidate(() => {
                this.apiDelete.filters = model;
                this.apiDelete.delete().then(data => {
                    this.onAfterDelete(data);
                    this.executeFilter();
                    this.modalDeleteOpen = false;
                }, err => { });
            });
        },

        executeFilter: function () {
            this.executeLoad(this.filter);
        },
        executePageChanged: function (index) {
            this.filter.pageIndex = index;
            this.executeLoad(this.filter);
        },
        executeOrderBy: function (field) {
            let type = 0;
            if (this.filter.orderByType == 0) type = 1;
            this.filter.orderFields = [field];
            this.filter.orderByType = type;
            this.filter.isOrderByDynamic = true;
            this.executeLoad(this.filter);
        },
        executeLoad: function (filter) {
            this.apiFilter.filters = filter;
            this.apiFilter.get().then(data => {
                this.result.total = data.summary.total;
                this.result.itens = data.dataList;
            });
        },


        formValidate: function (action) {
            bus.$emit('validate');
            setTimeout(() => {
                if (!this.errors.items || this.errors.items.length == 0)
                    action();
            }, 500)
        },
        formIsValid: function () {
            return this.errors.items.length == 0;
        },
        resetForm: function () {
            this.errors.items = [];
        },
        registerErrorsEvent: function () {
            bus.$on('errors-changed', (_errors) => {
                this.errors.items = [];
                _errors.forEach(error => { this.errors.add(error.field, error.msg) });
            });
        }

    },

    mounted() {
        this.executeFilter();
        this.registerErrorsEvent();
    }
}