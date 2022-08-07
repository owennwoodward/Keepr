<template>
    <div class="main-container">

        <div class="row m-4">
            <div class="col-12 d-flex justify-content-between">
                <h1 class="text-dark">{{ vault.name }}</h1>
                <button class="btn btn-outline-dark" v-if="vault.creator?.id == account.id" @click="deleteVault">
                    Delete Vault
                </button>
            </div>
            <div class="col-12 mt-3 text-dark">
                <h5>Keeps: <span>{{ keeps.length }} </span></h5>
            </div>
        </div>
        <div class="masonry-frame ms-4">
            <div v-for="k in keeps" :key="k.id" class="" title="Go to Keep">
                <Keep :keep="k" />
                <!-- <div class="row"> -->
                <div class="col-12 delete-button" v-if="vault.creatorId == account.id">
                    <i class="btn mdi mdi-delete mt-1 " title="Remove keep from vault"
                        @click="removeKeep(k.vaultKeepId, k.id)">

                    </i>
                </div>
                <!-- </div> -->
            </div>
        </div>
    </div>
</template>


<script>
import { computed, onMounted } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
import { vaultsService } from '../services/VaultsService'
import { vaultKeepsService } from '../services/VaultKeepsService'
import { router } from '../router'

export default {

    setup() {
        const route = useRoute()
        const router = useRouter()
        onMounted(async () => {
            try {
                await vaultsService.activeVault(route.params.vaultId)
                await vaultsService.getKeepFromVault(AppState.activeVault.id)
                if (AppState.activeVault.name == undefined) {
                    router.push({ name: 'Home' })
                    Pop.toast("You can't view private vaults")
                }
            }
            catch (error) {
                logger.error(error);
                Pop.toast("This Vault is private")
                router.push({ name: 'Home' })
            }
        })

        return {
            async deleteVault() {
                try {
                    if (await Pop.confirm("Do you want to delete this vault")) {
                        await vaultsService.deleteVault(AppState.activeVault.id)
                        Pop.toast("Vault deleted, sending you Home", "success")
                        router.push({
                            name: 'Home'
                        })
                    }
                } catch (error) {
                    logger.error(error)
                    Pop.toast(error.message, 'error')
                }
            },

            async removeKeep(vaultKeepId, keepId) {
                if (await Pop.confirm("Are you sure you want to remove this keep?")) {
                    await vaultKeepsService.removeKeep(vaultKeepId, keepId)
                }
            },

            account: computed(() => AppState.account),
            vault: computed(() => AppState.activeVault),
            keeps: computed(() => AppState.keeps),
            profile: computed(() => AppState.profile)
        }
    }
}
</script>


<style lang="scss" scoped>
.masonry-frame {
    columns: 4 200px;
    column-gap: 1rem;

    div {
        break-inside: avoid;
    }
}

.green-color {
    color: rgb(128, 236, 173);
}

.main-container {
    position: relative;
    text-align: left;
    color: white;

}

.delete-button {
    position: relative;
    bottom: 50px;
    left: 16px;
}
</style>