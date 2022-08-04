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
                    <i class="mdi mdi-eye h3 green-color"> </i><span class="ms-2 me-2 h5 "> {{ keep.views }} </span>
                    <i class="mdi mdi-book h3 green-color"> </i><span class="ms-2 h5"> {{ keep.kept }} </span>
                    <h1 class="pb-2 pt-5">{{ keep.name }}</h1>
                    <p class=" h3 pt-5">
                        {{ keep.description }}

                    </p>
                    <p class="border-bottom border-secondary pt-5"></p>

                    <div class=" row justify-content-evenly ">

                        <div class="col-3 pe-4 ">

                            <div class="me-2 mt-3">
                                <div class="dropdown">
                                    <button class="btn green-color border dropdown-toggle" data-bs-toggle="dropdown">
                                        Add to Vault
                                    </button>
                                    <ul class="dropdown-menu">
                                        <li class="dropdown-item " v-for="v in vaults" :key="v.id"
                                            @click="addKeepToVault(v.id)">{{ v.name }}</li>
                                    </ul>
                                </div>
                                <!-- <form class="vault-control">
                                    <button type="submit" class="btn btn-primary p-2 " @click.stop="addToVault"><b>Add
                                            to
                                            Vault</b> <select class="">
                                            <option class="" v-for="v in vaults" :key="v.id" :value="v.id">
                                                {{ v.name }}
                                            </option>
                                        </select></button>
                                </form> -->
                            </div>
                        </div>
                        <!-- v-if="activeKeep.creatorId == account.id" try to get this to work, to hide delete button if not urs. -->
                        <div class="col-2 ms-3 btn">
                            <p class="mdi mdi-delete h1" v-if="keep.creatorId == account.id" @click="deleteKeep"
                                title="Delete this Keep"></p>
                        </div>
                        <div class="col-6 mt-1">
                            <img class="profile-pic p-2 selectable rounded-pill" @click.stop="takeToProfile"
                                title="Go to Profile" :src="keep.creator?.picture" /><span class="h6">{{
                                        keep.creator?.name
                                }}</span>

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
import { vaultKeepsService } from '../services/VaultKeepsService'
import { vaultsService } from '../services/VaultsService'

export default {
    setup() {
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
                        Pop.toast('Keep not deleted. Be Careful Next Time', 'success')
                        Modal.getOrCreateInstance(document.getElementById('keep-modal')).show()
                    }
                } catch (error) {
                    logger.error(error)
                    Pop.toast(error.message, 'error')
                }
            },

            async addKeepToVault(id) {
                try {
                    let vaultKeep = { keepId: AppState.activeKeep.id, vaultId: id }
                    await vaultKeepsService.addKeepToVault(vaultKeep)
                    AppState.activeKeep.kept++
                    Pop.toast('Keep added to vault', 'success')
                } catch (error) {
                    logger.error(error)
                    Pop.toast(error.message, 'error')
                }
            },
            keep: computed(() => AppState.activeKeep),
            vault: computed(() => AppState.activeVault),
            vaults: computed(() => AppState.profileVaults),
            profile: computed(() => AppState.profile),
            account: computed(() => AppState.account)
        }
    }
}
</script>


<style lang="scss" scoped>
.keep-size {
    width: 50vh;
    height: 50vh;
}

.standard-length {
    width: 7rem;
    margin-left: 1rem;
    border-radius: 4px;
}

.vault-control {
    display: flex;
    align-items: center;
}

.green-color {
    color: rgb(128, 236, 173);
}
</style>