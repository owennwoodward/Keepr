<template>
    <div class="container-fluid pb-5 ms-4 ">
        <div class="row ">
            <div class="col-4 mt-4 ms-4">
                <img :src="profile.picture" class="rounded-pill">
                <div class="col-8">
                    <h1 class="pt-4 ms-5">{{ profile.name }}</h1>
                    <h2 class="ms-5">Vaults: {{ vaults.length }}</h2>
                    <h2 class="ms-5">Keeps: {{ keeps.length }}</h2>

                </div>
            </div>
        </div>
    </div>
    <div class="container-fluid ms-4">
        <div class="row ms-4 pb-4">
            <h1 class="">Vaults <span class="mdi mdi-plus selectable" v-if="profile.id == account.id"
                    @click="getVaultForm">
                </span></h1>
            <div class="col-3" v-for="v in vaults" :key="v.id">
                <Vault :vault="v" />
            </div>

        </div>

    </div>

    <div class="container-fluid masonry-frame">
        <h1 class="ps-5 ms-3">Keeps <span class="mdi mdi-plus selectable" v-if="profile.id == account.id"
                @click="getKeepForm"></span></h1>
        <div class="pt-5" v-for="k in keeps" :key="k.id">
            <Keep :keep="k" />
        </div>
    </div>
</template>


<script>
import { computed, onMounted } from 'vue'
import { useRoute } from 'vue-router'
import { profilesService } from '../services/ProfilesService'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
import Keep from '../components/Keep.vue'
import { AppState } from '../AppState'
import { Modal } from 'bootstrap'

export default {
    setup() {
        const route = useRoute();
        onMounted(async () => {
            try {
                await profilesService.getMyProfile(route.params.id);
                await profilesService.getKeeps(route.params.id);
                await profilesService.getVaults(route.params.id);
            }
            catch (error) {
                logger.error(error);
                Pop.toast(error.message, "error");
            }
        });
        return {
            profile: computed(() => AppState.profile),
            keeps: computed(() => AppState.keeps),
            vaults: computed(() => AppState.vaults),
            account: computed(() => AppState.account),
            async getVaultForm() {
                try {
                    Modal.getOrCreateInstance(document.getElementById('createVault-modal')).show();
                }
                catch (error) {
                    logger.error(error);
                    Pop.toast(error.message, "error");
                }
            },

            async getKeepForm() {
                try {
                    Modal.getOrCreateInstance(document.getElementById('createKeep-modal')).show();
                }
                catch (error) {
                    logger.error(error);
                    Pop.toast(error.message, "error");
                }
            }
        };
    },
    components: { Keep }
}
</script>


<style lang="scss" scoped>
.masonry-frame {
    columns: 3;

    div {
        break-inside: avoid;
    }
}
</style>