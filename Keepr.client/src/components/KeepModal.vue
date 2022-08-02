<template>
    <Modal id="keep-modal">
        <template #modal-title></template>
        <template #modal-body>
            <!-- <div class="container d-flex"> -->
            <div class="row">

                <div class="col-6">

                    <img class="img-fluid keep-size" :src="keep.img">
                </div>
                <div class="col-6 text-center pt-5">
                    <h1 class="pb-2 ">{{ keep.name }}</h1>
                    <p class=" h3 pt-5">
                        {{ keep.description }}

                    </p>
                    <p class="border-bottom border-secondary pt-5"></p>

                    <div class=" row justify-content-between">

                        <div class="col-4">
                            <div class="dropdown">
                                <button class="btn btn-secondary dropdown-toggle" type="button" id="dropdownMenuButton"
                                    data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                    VAULT
                                </button>
                                <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                                    <a class="dropdown-item" href="#">Action</a>
                                    <a class="dropdown-item" href="#">Another action</a>
                                    <a class="dropdown-item" href="#">Something else here</a>
                                </div>
                            </div>
                        </div>
                        <div class="col-2 btn btn-primary">
                            <p class="mdi mdi-delete h1" @click="deleteKeep"></p>
                        </div>
                        <!-- v-if="activeKeep.creatorId == account.id" try to get this to work, to hide delete button if not urs. -->
                        <div class="col-4">
                            <img class="profile-pic p-2 selectable rounded-pill" @click.stop="takeToProfile"
                                :src="keep.creator?.picture" />

                        </div>
                    </div>
                </div>
            </div>
        </template>
    </Modal>
</template>


<script>
import { Modal } from 'bootstrap'
import { computed } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
import { keepsService } from '../services/KeepsService'

export default {
    setup(props) {
        const router = useRouter()

        return {
            takeToProfile() {
                Modal.getOrCreateInstance(document.getElementById('keep-modal')).hide()
                router.push({
                    name: 'Profile',
                    params: {
                        id:
                            AppState.activeKeep.creator?.id
                    }
                })
            },

            async deleteKeep() {
                try {
                    Modal.getOrCreateInstance(document.getElementById('keep-modal')).hide()
                    if (await Pop.confirm('Are you sure you want to delete this?')) {
                        await keepsService.deleteKeep()
                        Pop.toast('Keep deleted', 'success')
                    } else {
                        Modal.getOrCreateInstance(document.getElementById('keep-modal')).toggle()
                    }
                } catch (error) {
                    logger.error(error)
                    Pop.toast(error.message, 'error')
                }
            },
            keep: computed(() => AppState.activeKeep),
            vault: computed(() => AppState.profileVaults),
            profile: computed(() => AppState.profile),
            account: computed(() => AppState.account),
        }
    }
}
</script>


<style lang="scss" scoped>
.keep-size {
    width: 50vh;
    height: 50vh;
}
</style>