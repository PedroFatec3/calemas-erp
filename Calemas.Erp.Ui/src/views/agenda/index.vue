﻿<template>
    <div class="wrapper">

        <div class="row" style="margin-bottom: 1rem;margin-top: -0.5rem;">
            <div class="col-xs-12 col-sm-7 col-md-7 col-lg-8">
                <h6 class="page-title txt-color-blueDark">
                    <i class="fa-fw fa fa-calendar"></i>
                    Agenda
                </h6>
            </div>
            <div class="col-xs-12 col-sm-5 col-md-5 col-lg-4 text-right">
                <div class="btn-group">
                    <a href="javascript:history.back()" class="btn btn-secondary btn-sm pull-right header-btn hidden-mobile">
                        <i class="fa fa-reply"></i> Voltar
                    </a>
                    <button @click="openFilter()" class="btn btn-primary btn-sm pull-right header-btn">
                        <i class="fa fa-filter"></i> Filtros
                    </button>
                </div>
            </div>
        </div>

        <div class="row animated fadeIn" v-if="filterPartialIsOpen">
            <div class="col-sm-12">
                <div class="card">
                    <div class="card-header">
                        <strong>Filtros</strong>
                    </div>
                    <div class="card-block">
                        <form v-on:keypress.enter.prevent="executeFilter()">
                            <filter-partial :filter="filter" />
                        </form>
                    </div>
                    <div class="card-footer text-right">
                        <button type="submit" class="btn btn-primary" @click="executeFilter()">
                            <i class="fa fa-search"></i> Filtrar
                        </button>
                    </div>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-sm-12">
                <big-calendar :events="events" locale="pt" :weekNames="weekNames" :monthNames="monthNames" @eventClick="eventClickAgenda"></big-calendar>
            </div>
        </div>

        <modal title="Cadastro de Agenda" v-model="modalCreateIsOpen" effect="fade/zoom" type="modal-success" :large="true">
            <div slot="modal-header" class="modal-header">
                <h4 class="modal-title">Cadastro de Agenda</h4>
                <button type="button" class="close" @click="closeCreate()"><span>&times;</span></button>
            </div>
            <form v-on:keypress.enter.prevent="executeCreate(model)" id="form-create" novalidate>
                <form-partial :model="model" />
            </form>
            <div slot="modal-footer" class="modal-footer">
                <button type="button" class="btn btn-default" @click="closeCreate()">Fechar</button>
                <button type="button" class="btn btn-success" @click="executeCreate(model)">
                    <i class="fa fa-check"></i> Salvar
                </button>
            </div>
        </modal>

        <modal title="Edição de Agenda" v-model="modalEditIsOpen" effect="fade/zoom" type="modal-primary" :large="true">
            <div slot="modal-header" class="modal-header">
                <h4 class="modal-title">Edição de Agenda</h4>
                <button type="button" class="close" @click="closeEdit()"><span>&times;</span></button>
            </div>
            <form v-on:keypress.enter.prevent="executeEdit(model)" id="form-edit" novalidate>
                <form-partial :model="model" />
            </form>
            <div slot="modal-footer" class="modal-footer">
                <button type="button" class="btn btn-default" @click="closeEdit()">Fechar</button>
                <button type="button" class="btn btn-success" @click="executeEdit(model)">
                    <i class="fa fa-pencil"></i> Alterar
                </button>
            </div>
        </modal>
        
    </div>
</template>
<script>

    import Vue from 'vue'
    const _moment = require('moment')
    require('moment/locale/pt')
    Vue.use(require('vue-moment'), { _moment });

    import bigCalendar from 'vue-big-calendar'

    import crudBase from '../../common/mixins/crud'

    import formPartial from './form.partial'
    import filterPartial from './filter.partial'

    export default {
        name: "agenda",
        components: { formPartial, filterPartial, bigCalendar },
        mixins: [crudBase],
        data() {
            return {
                filter: {
                    pageSize: 50,
                },
                resource: "agenda",
                weekNames: ["Dom", "Seg", "Ter", "Qua", "Qui", "Sex", "Sab"],
                monthNames: ["Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho", "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro"]
            }
        },
        computed: {
            events: function () {
                let itens = [];
                for (var i = 0; i < this.result.itens.length; i++) {
                    let item = this.result.itens[i];
                    itens.push({
                        title: item.nome,
                        description: item.descricao,
                        start: item.dataInicio,
                        end: item.dataFim,
                        color: item.cor.hash,
                        cssClass: 'red',
                        YOUR_DATA: item,
                    })
                }
                
                return itens;
            }
        },
        methods: {
            eventClickAgenda: function (a, b, c) {
                this.openEdit(a.YOUR_DATA.agendaId, a.YOUR_DATA);
            },
        }

    }
</script>
<style>
    .comp-full-calendar {
        max-width: none !important;
    }
</style>