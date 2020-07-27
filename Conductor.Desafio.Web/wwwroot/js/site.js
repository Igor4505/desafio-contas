function modalCadastro() {
    $('#modalCadastro').modal({
        fadeDuration: 130,
        showClose: false
    });
}

//MODAL ALERTA
function alerta() {
    $('#alerta').modal({
        fadeDuration: 130,
        showClose: false
    });
}

//MODAL EDITAR
function editPessoa() {
    $('#editPessoa').modal({
        fadeDuration: 130,
        showClose: false
    });
}

//MODAL EDITAR
function modalExcluir() {
    $('#modalExcluir').modal({
        fadeDuration: 130,
        showClose: false
    });
}

//MODAL ADD CONTA
function addConta() {
    $('#addConta').modal({
        fadeDuration: 130,
        showClose: false
    });
}

//MODAL EXCLUIR CONTA
function delConta(id) {
    $('#formDelConta').attr('action', 'Contas/ExcluirConta/'+id);
    $('#delConta').modal({
        fadeDuration: 130,
        showClose: false
    });
}

//MODAL EXCLUIR TRANSAÇÃO
function delTransacao(id) {
    $('#formDelTransacao').attr('action', 'Transacoes/ExcluirTransacao/' + id);
    $('#delTransacao').modal({
        fadeDuration: 130,
        showClose: false
    });
}

//MODAL DESATIVAR CONTA
function desativarConta(id) {
    $('#formDesConta').attr('action', 'Contas/DesativarConta/' + id);
    $('#desativarConta').modal({
        fadeDuration: 130,
        showClose: false
    });
}

//MODAL ADICIONAR TRANSACAO
function addTransacao(id) {
    $('#addTransacao').modal({
        fadeDuration: 130,
        showClose: false
    });
}



