<template>
    <div class="wrapper">

        <div class="row" style="margin-bottom: 1rem;margin-top: -0.5rem;">
            <div class="col-xs-12 col-sm-7 col-md-7 col-lg-8">
                <h6 class="page-title txt-color-blueDark">
                    <i class="fa-fw fa fa-home"></i>
                    Cadastros
                    <span>Manutenção de Movimentação de estoque</span>
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
                <div class="card">
                    <div class="card-header">
                        <strong>Resultado</strong>
                    </div>
                    <div class="card-block no-padding">
                        <table class="table table-striped table-sm">
                            <thead class="">
                                <tr>
                                    <th>Categoria<button @click="executeOrderBy('categoriaEstoque.nome')" class="btn btn-sm btn-link no-border pull-right hide"><i class="fa fa-sort"></i></button></th>
                                    <th>Nome<button @click="executeOrderBy('nome')" class="btn btn-sm btn-link no-border pull-right hide"><i class="fa fa-sort"></i></button></th>
                                    <th>Modelo<button @click="executeOrderBy('modelo')" class="btn btn-sm btn-link no-border pull-right hide"><i class="fa fa-sort"></i></button></th>
                                    <th>Fabricante<button @click="executeOrderBy('fabricante')" class="btn btn-sm btn-link no-border pull-right hide"><i class="fa fa-sort"></i></button></th>
                                    <th>Qtde. Mínima<button @click="executeOrderBy('quantidadeMinima')" class="btn btn-sm btn-link no-border pull-right hide"><i class="fa fa-sort"></i></button></th>
                                    <th>Qtde. Atual<button @click="executeOrderBy('quantidade')" class="btn btn-sm btn-link no-border pull-right hide"><i class="fa fa-sort"></i></button></th>
                                    <th>Ativo <button @click="executeOrderBy('ativo')" class="btn btn-sm btn-link no-border pull-right hide"><i class="fa fa-sort"></i></button></th>
                                    <th class="text-center" width="150"><i class="fa fa-cog"></i></th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr v-for="item in result.itens" class="animated fadeIn">
                                    <td>{{ item.categoriaEstoque.nome }}</td>
                                    <td>{{ item.nome }}</td>
                                    <td>{{ item.modelo }}</td>
                                    <td>{{ item.fabricante }}</td>
                                    <td>{{ item.quantidadeMinima }}</td>
                                    <td>{{ item.quantidade }}</td>
                                    <td><span class="badge badge-pill" v-bind:class="{ 'badge-success': item.ativo, 'badge-danger': !item.ativo }">{{item.ativo ? 'Sim' : 'Não'}}</span></td>
                                    <td class="text-center">
                                        <router-link :to="{ name: 'estoquemovimentacaodetails', params: { id: item.estoqueId }}" v-tooltip.left="'Histórico'" class="btn btn-sm btn-info">
                                            <i class="fa fa-history"></i>
                                        </router-link>
                                        <button type="button" class="btn btn-sm btn-danger" v-tooltip.left="'Realizar uma retirada'" @click="openCreate({ estoqueId: item.estoqueId, entrada: false })">
                                            <i class="fa fa-minus"></i>
                                        </button>
                                        <button type="button" class="btn btn-sm btn-success" v-tooltip.left="'Realizar uma entrada'" @click="openCreate({ estoqueId: item.estoqueId, entrada: true })">
                                            <i class="fa fa-plus"></i>
                                        </button>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <div class="card-block no-padding">
                        <pagination :total="result.total" :page-size="filter.pageSize" :callback="executePageChanged"></pagination>
                    </div>
                </div>
            </div>
        </div>

        <modal title="Cadastro de Estoque" v-model="modalCreateIsOpen" effect="fade/zoom" type="modal-success" :large="true">
            <div slot="modal-header" class="modal-header">
                <h4 class="modal-title">Cadastro de Movimentação de estoque</h4>
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

    </div>
</template>
<script>


    import crudBase from '../../common/mixins/crud'

    import formPartial from './form.partial'
    import filterPartial from './filter.partial'

    export default {
        name: "estoquemovimentacao",
        components: { formPartial, filterPartial },
        mixins: [crudBase],
        data() {
            return {
                resource: "estoque",
                resources: {
                    create: "estoquemovimentacao"
                }
            }
        }
    }
</script>
