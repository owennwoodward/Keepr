<template>
    <Modal id="createVault-modal">
        <template #modal-title>
            <p class="h1">New Vault</p>
        </template>
        <template #modal-body>

            <form class="row" action="" @submit="vaultCreate">
                <div class="col-md-12">
                    <label for="Vault Name" title="Vault Name">Name</label>
                    <input class="form-control" type="text" v-model="editable.name" />
                </div>
                <!-- <div class="col-4">
                    <label for="">Description</label>
                    <textarea>
                    <input class="form-control" type="text" rows="5" v-model="editable.description" /></textarea>
                </div> -->
                <div class="col-md-12">
                    <label for="">Image</label>
                    <input class="form-control" type="text" v-model="editable.img" />
                </div>
                <div class="col-12">
                    <label for="">Description</label>
                    <textarea class="form-control" name="" id="" cols="30" rows="5"
                        v-model="editable.description"></textarea>
                </div>
                <div class="col-md-12">
                    <label for="">Private?</label>
                    <input class="form-control" type="checkbox" v-model="editable.isPrivate" />
                </div>
                <div class="col-4 pt-3">
                    <button class="btn btn-primary" data-bs-dismiss="modal">Submit</button>
                </div>
            </form>

        </template>

    </Modal>
</template>


<script>
import { ref } from 'vue'
import { useRoute } from 'vue-router'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
import { vaultsService } from '../services/VaultsService'
import { Modal } from 'bootstrap'

export default {
    setup() {
        const editable = ref({})
        const route = useRoute()
        return {
            editable,
            async vaultCreate() {
                try {
                    await vaultsService.vaultCreate(editable.value)
                    Modal.getOrCreateInstance(document.getElementById('createVault-modal')).hide();
                    editable.value = {}
                } catch (error) {
                    logger.error(error)
                    Pop.toast(error.message, 'error')
                }
            }
        }
    }
}
</script>


<style lang="scss" scoped>
</style>