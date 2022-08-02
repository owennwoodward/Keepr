<template>
    <Modal id="createKeep-modal">
        <template #modal-title>
            <p class="h1">New Keep</p>
        </template>
        <template #modal-body>

            <form class="row" action="" @submit="keepCreate">
                <div class="col-md-12">
                    <label for="Keep Name" title="Keep Name">Name</label>
                    <input class="form-control" type="text" v-model="editable.name" />
                </div>
                <div class="col-md-12">
                    <label for="">Image</label>
                    <input class="form-control" type="text" v-model="editable.img" />
                </div>
                <div class="col-12">
                    <label for="">Description</label>
                    <textarea class="form-control" name="" id="" cols="30" rows="5"
                        v-model="editable.description"></textarea>
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
import { keepsService } from '../services/KeepsService'

export default {
    setup() {
        const editable = ref({})
        const route = useRoute()
        return {
            editable,
            async keepCreate() {
                try {
                    await keepsService.keepCreate(editable.value)
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